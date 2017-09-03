using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace TestAnimal
{
    class MainViewModel : ViewModelBase
    {
        RepositoryAnimal cashe = new RepositoryAnimal();
        

         AnimalType selectedType = null;//выбранный элемент
         Animal selectedAnimal = null;//выбранное животное
         List<Animal> animalList = null;
        AnimalType selectedTypeInsert = null;        

        Animal insert = null;
        ActionViewModel<string> addCommand;
        ActionViewModel<string> insertCommand;
        ActionViewModel<string> loadCommand;

        List<ActionViewModel<string>> actionList = null;//список кнопок

        public MainViewModel()
        {
            animalType = new List<AnimalType>();
            animalType.Add(new AnimalType(1, "Животные"));
            animalType.Add(new AnimalType(2, "Насекомые"));
            animalType.Add(new AnimalType(3, "Бабочки"));
            insert = new Animal();

            actionList = new List<ActionViewModel<string>>();
           
            actionList.Add(LoadCommand);
            actionList.Add(InsertCommand);

            /*
            actionList.Add(new ActionViewModel<string>("3",(param) =>
            {
                MessageBox.Show("3");
            })
            );
            */     
          
        }

        public List<AnimalType> animalType //список типов комбобокс
        {
            get; set;
        }

        public ActionViewModel<string> AddCommand 
        { 
            get {
                return addCommand ?? (addCommand = new ActionViewModel<string>("Добавить",(param) =>
                {
                    new RepositoryAnimal().InsertAnimal(insert);
                }));
            }               
        }


        public ActionViewModel<string> InsertCommand
        {
            get
            {
                return insertCommand ?? (insertCommand = new ActionViewModel<string>("Insert", (param) =>
                {
                    Console.WriteLine("Вызов метода");
                    await RepositoryAnimal.InsertLinq(insert);
                    Console.WriteLine("Метод завершен");
                }));
            }                       
        }


        public ActionViewModel<string> LoadCommand
        {
            get
            {
                return loadCommand ?? (loadCommand = new ActionViewModel<string>("Load", (param) =>
                {
                List<Animal> l = RepositoryBase<Animal>.Load(x => x.mass > 10);
                    foreach (var an in l)
                    {
                        MessageBox.Show(an.id + " " + an.mass + " " + an.name);
                    }
                }));
            }
        }

        public List<ActionViewModel<string>> ActionList
        {
            get
            {
                return actionList;
            }
            set
            {
                actionList = value;
                OnPropertyChanged();
            }
        }


        public AnimalType SelectedType
        {

            get
            {
                animalList = null;
                OnPropertyChanged("AnimalList");

                return selectedType;
            }
            set
            {
                selectedType = value;
                OnPropertyChanged();
            }
        }

      

        public Animal SelectedAnimal
        {
            get
            {
                return selectedAnimal;
            }
            set
            {
                selectedAnimal = value;
                OnPropertyChanged();
            }
        }
        public List<Animal> AnimalList
        {
            get
            {
                if (selectedType != null)
                    animalList = cashe.LoadByType(selectedType.idType);//загрузка по типу
                    
                return animalList;
            }
            set
            {
                animalList = value;
                OnPropertyChanged();
            }

        }
       
        //для добавления

        public Animal Insert
        {
            get
            {
                return insert;
            }
            set
            {
                insert = value;
                OnPropertyChanged();
            }
        }

        public AnimalType SelectedTypeInsert
        {

            get
            {
                Animal an = insert;
                if (selectedTypeInsert!=null)
                {/*
                    if (selectedTypeInsert.idType == 1) insert = new Animal();
                    if (selectedTypeInsert.idType == 2) insert = new Insect();
                    if (selectedTypeInsert.idType == 3) insert = new Butterfly();
                    */
                    insert.id = an.id;
                    insert.mass = an.mass;
                    insert.name = an.name;
                    insert.typeAnimal = selectedTypeInsert.idType;
                }

                return selectedTypeInsert;
            }
            set
            {
                selectedTypeInsert = value;
                OnPropertyChanged();
            }
        }       
     
    }
}
