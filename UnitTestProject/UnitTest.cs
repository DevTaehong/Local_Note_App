
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assignment2.Models;
using Assignment2.Repositories2;
using Assignment2.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{

    [TestClass]
    public class UnitTest1
    {
        public ObservableCollection<TitleModel> Titles;
        public List<TitleModel> _allTitles = new List<TitleModel>();


        [TestMethod]
        public void TestCollection()
        {
            TitleModel titleModel = new TitleModel("title", new TextModel("text"));
            _allTitles.Add(titleModel);

            Assert.AreEqual(1, _allTitles.Count);
        }

        [TestMethod]
        public void TestAlreadyExiststNote()
        {
            TitleModel titleModel = new TitleModel("title", new TextModel("text"));
            bool notExsits = _allTitles.Exists(x => x.NoteTitle == "title");
            

            Assert.AreNotEqual(true, notExsits);
        }
    }
}
