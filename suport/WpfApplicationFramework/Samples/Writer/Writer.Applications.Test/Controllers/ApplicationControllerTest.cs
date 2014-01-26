﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Waf.Applications;
using System.Waf.Applications.Services;
using System.Waf.UnitTesting.Mocks;
using Waf.Writer.Applications.Controllers;
using Waf.Writer.Applications.Properties;
using Test.Writer.Applications.Services;
using Test.Writer.Applications.Views;
using Waf.Writer.Applications.ViewModels;
using Waf.Writer.Applications.Views;

namespace Test.Writer.Applications.Controllers
{
    [TestClass]
    public class ApplicationControllerTest : TestClassBase
    {
        [TestMethod]
        public void ControllerLifecycle()
        {
            IApplicationController applicationController = Container.GetExportedValue<IApplicationController>();
            
            applicationController.Initialize();
            
            MockShellView shellView = (MockShellView)Container.GetExportedValue<IShellView>();
            ShellViewModel shellViewModel = ViewHelper.GetViewModel<ShellViewModel>(shellView);
            Assert.IsNotNull(shellViewModel.ExitCommand);

            applicationController.Run();

            Assert.IsTrue(shellView.IsVisible);
            MainViewModel mainViewModel = Container.GetExportedValue<MainViewModel>();
            Assert.AreEqual(mainViewModel.View, shellViewModel.ContentView);

            shellViewModel.ExitCommand.Execute(null);
            Assert.IsFalse(shellView.IsVisible);

            applicationController.Shutdown();
        }

        [TestMethod]
        public void OpenFileViaCommandLine()
        {
            MockEnvironmentService environmentService = Container.GetExportedValue<MockEnvironmentService>();
            environmentService.DocumentFileName = "Document.mock";

            IApplicationController applicationController = Container.GetExportedValue<IApplicationController>();
            applicationController.Initialize();

            
            // Open the 'Document.mock' file
            applicationController.Run();

            // Open a file with an unknown file extension and check if an error message is shown.
            environmentService.DocumentFileName = "Unknown.fileExtension";
            MockMessageService messageService = Container.GetExportedValue<MockMessageService>();
            messageService.Clear();
            
            applicationController.Run();

            Assert.AreEqual(MessageType.Error, messageService.MessageType);
            Assert.IsFalse(string.IsNullOrEmpty(messageService.Message));
        }

        [TestMethod]
        public void SaveChangesTest()
        {
            IApplicationController applicationController = Container.GetExportedValue<IApplicationController>();
            applicationController.Initialize();
            applicationController.Run();

            ShellViewModel shellViewModel = Container.GetExportedValue<ShellViewModel>();
            shellViewModel.FileService.NewCommand.Execute(null);
            
            MainViewModel mainViewModel = Container.GetExportedValue<MainViewModel>();
            RichTextViewModel richTextViewModel = ViewHelper.GetViewModel<RichTextViewModel>((IView)mainViewModel.ActiveDocumentView);
            richTextViewModel.Document.Modified = true;

            bool showDialogCalled = false;
            MockSaveChangesView saveChangesView = Container.GetExportedValue<MockSaveChangesView>();
            MockSaveChangesView.ShowDialogAction = (view) =>
            {
                showDialogCalled = true;
                Assert.IsTrue(ViewHelper.GetViewModel<SaveChangesViewModel>(view).Documents.SequenceEqual(
                    new[] { richTextViewModel.Document }));
                view.Close();
            };

            // When we try to close the ShellView then the ApplicationController shows the SaveChangesView because the
            // modified document wasn't saved.
            shellViewModel.ExitCommand.Execute(null);
            Assert.IsTrue(showDialogCalled);
            MockShellView shellView = (MockShellView)Container.GetExportedValue<IShellView>();
            Assert.IsTrue(shellView.IsVisible);

            showDialogCalled = false;
            MockSaveChangesView.ShowDialogAction = (view) =>
            {
                showDialogCalled = true;
                view.ViewModel.YesCommand.Execute(null);
            };

            MockFileDialogService fileDialogService = (MockFileDialogService)Container.GetExportedValue<IFileDialogService>();
            fileDialogService.Result = new FileDialogResult();

            // This time we let the SaveChangesView to save the modified document
            shellViewModel.ExitCommand.Execute(null);
            Assert.IsTrue(showDialogCalled);
            Assert.AreEqual(FileDialogType.SaveFileDialog, fileDialogService.FileDialogType);
            Assert.IsFalse(shellView.IsVisible);

            MockSaveChangesView.ShowDialogAction = null;
        }

        [TestMethod]
        public void SettingsTest()
        {
            Settings.Default.IsUpgradeNeeded = false;
            Settings.Default.Culture = "de-DE";
            Settings.Default.UICulture = "de-AT";

            IApplicationController applicationController = Container.GetExportedValue<IApplicationController>();
            
            Assert.AreEqual(new CultureInfo("de-DE"), CultureInfo.CurrentCulture);
            Assert.AreEqual(new CultureInfo("de-AT"), CultureInfo.CurrentUICulture);

            applicationController.Initialize();
            applicationController.Run();

            ShellViewModel shellViewModel = Container.GetExportedValue<ShellViewModel>();
            shellViewModel.EnglishCommand.Execute(null);
            Assert.AreEqual(new CultureInfo("en-US"), shellViewModel.NewLanguage);

            bool settingsSaved = false;
            Settings.Default.SettingsSaving += (sender, e) =>
            {
                settingsSaved = true;
            };

            applicationController.Shutdown();
            Assert.AreEqual("en-US", Settings.Default.UICulture);
            Assert.IsTrue(settingsSaved);

            // Restore the culture settings
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }
}
