﻿using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data.Entity;
using Waf.BookLibrary.Library.Applications.Data;
using Waf.BookLibrary.Library.Domain;

namespace Waf.BookLibrary.Library.Applications.Services
{
    [Export(typeof(IEntityService)), Export]
    internal class EntityService : IEntityService
    {
        private ObservableCollection<Book> books;
        private ObservableCollection<Person> persons;


        public BookLibraryContext BookLibraryContext { get; set; }
        
        public ObservableCollection<Book> Books
        {
            get 
            {
                if (books == null && BookLibraryContext != null)
                {
                    BookLibraryContext.Set<Book>().Include(x => x.LendTo).Load();
                    books = BookLibraryContext.Set<Book>().Local;
                }
                return books;
            }
        }

        public ObservableCollection<Person> Persons
        {
            get 
            {
                if (persons == null && BookLibraryContext != null)
                {
                    BookLibraryContext.Set<Person>().Load();
                    persons = BookLibraryContext.Set<Person>().Local;
                }
                return persons;
            }
        }
    }
}
