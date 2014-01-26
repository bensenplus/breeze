﻿using System;

namespace System.Waf.UnitTesting.Mocks
{
    /// <summary>
    /// Base class for a mock dialog view implementation.
    /// </summary>
    /// <typeparam name="TMockView">The type of the concrete mock dialog view.</typeparam>
    public abstract class MockDialogView<TMockView> : MockView where TMockView : MockDialogView<TMockView>
    {
        /// <summary>
        /// Gets or sets a delegate which is called when this view should be shown.
        /// </summary>
        public static Action<TMockView> ShowDialogAction { get; set; }

        /// <summary>
        /// Gets the owner of this view.
        /// </summary>
        public object Owner { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this view is visible.
        /// </summary>
        public bool IsVisible { get; private set; }


        /// <summary>
        /// Shows the view. This method calls the ShowDialogAction.
        /// </summary>
        /// <param name="owner">The owner of this view.</param>
        public void ShowDialog(object owner)
        {
            this.Owner = owner;
            this.IsVisible = true;
            if (ShowDialogAction != null) { ShowDialogAction((TMockView)this); }
            this.Owner = null;
            this.IsVisible = false;
        }

        /// <summary>
        /// Close the view. This method sets IsVisible to false.
        /// </summary>
        public void Close()
        {
            this.Owner = null;
            this.IsVisible = false;
        }
    }
}
