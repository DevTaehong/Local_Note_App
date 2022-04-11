
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LocalNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
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

        [TestMethod]
        public void TestIfNoteContainsString()
        {
            TitleModel titleModel = new TitleModel("title", new TextModel("text"));


            StringAssert.Contains(titleModel.NoteTitle, "title");
        }

        [TestMethod]
        public void TestIsNotNullNote()
        {
            TitleModel titleModel = new TitleModel("title", new TextModel("text"));


            Assert.IsNotNull(titleModel);
        }

        [TestMethod]
        public void TestIsInstanceCorrectNote()
        {
            TitleModel titleModel = new TitleModel("title", new TextModel("text"));


            Assert.IsInstanceOfType(titleModel, typeof(TitleModel));
        }
    }
}
