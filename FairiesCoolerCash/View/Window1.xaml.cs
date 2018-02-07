using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FairiesCoolerCash.Business
{    
    /// <summary>
    /// Window1.xaml 的交互逻辑、、
    /// </summary>
    public partial class Window1 : Window
    {
        private int index;

        public Window1()
        {
            InitializeComponent();

            dogs.Add(new Dog("Dog1"));
            dogs.Add(new Dog("Dog2"));
            dogs.Add(new Dog("Dog3"));

            types.Add("EducationGrade1");
            types.Add("EducationGrade2");
            types.Add("EducationGrade3");

            this.listView1.ItemsSource = persons;
        }

        public List<String> types = new List<String>();
        public List<String> EducationTypes
        {
            get
            {
                return types;
            }
        }

        public BindingList<Dog> dogs = new BindingList<Dog>();
        public BindingList<Dog> Dogs
        {
            get
            {
                return dogs;
            }
        }

        public BindingList<Person> persons = new BindingList<Person>();
        public BindingList<Person> Persons
        {
            get
            {
                return persons;
            }
        }

        private void comboBoxInListView_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            box.ItemsSource = null;
            box.ItemsSource = dogs;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            persons.Add(new Person(++index));
        }
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            //persons.Add(new Person(++index));
            //Person p = (Person)this.listView1.SelectedItems[0];
            //MessageBox.Show(p.Sex.ToString());
            StringBuilder sb = new StringBuilder(33);
            sb.Append("123456");
            int st = 0;
            Int32 value=100;
            st = CardRef.CoolerPutCard(sb);
            st = CardRef.CoolerReadCard(sb, ref value);
            st = CardRef.CoolerRechargeCard(sb, 100);
            st = CardRef.CoolerConsumeCard(sb, 100);
            MessageBox.Show(CardRef.GetStr(st));
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            while (this.listView1.SelectedItems.Count > 0)
            {
                persons.Remove((Person)this.listView1.SelectedItems[0]);
            }
        }

        private void comboBoxInWnd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;

            this.stackPanel1.DataContext = null;
            this.stackPanel1.DataContext = box.SelectedItem;
        }
    }
    public class Dog
    {
        public Dog(string name)
        {
            this.name = name;
        }

        private Guid id = Guid.NewGuid();
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id
        {
            get { return id; }
        }

        private string name;
        /// <summary>
        /// Dog's Name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string test { get; set; }
    }
    public enum Sex
    {
        Male,
        Female,
        Unknow,
    }

    public enum EducationGrade
    {
        EducationGrade1,
        EducationGrade2,
        EducationGrade3,
    }

    public class Person : INotifyPropertyChanged
    {
        public Person(int index)
        {
            this.index = index;
            this.name = "Person" + index;
        }

        private Guid id = Guid.NewGuid();
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id
        {
            get
            {
                return id;
            }
        }

        private string name;
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }

        private int index;
        /// <summary>
        /// Index
        /// </summary>
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                if (index != value)
                {
                    index = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Index"));
                    }
                }
            }
        }

        private Sex sex = Sex.Unknow;
        public Sex Sex
        {
            get
            {
                return sex;
            }
            set
            {
                if (sex != value)
                {
                    sex = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Sex"));
                    }
                }
            }
        }

        private EducationGrade educationGrade;
        public EducationGrade EducationGrade
        {
            get
            {
                return educationGrade;
            }
            set
            {
                if (educationGrade != value)
                {
                    educationGrade = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("EducationGrade"));
                    }
                }
            }
        }

        private Guid myDog;
        public Guid MyDog
        {
            get
            {
                return myDog;
            }
            set
            {
                if (myDog != value)
                {
                    myDog = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("MyDog"));
                    }
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
    public class EducationGradeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EducationGrade getValue = (EducationGrade)value;

            switch (getValue)
            {
                case EducationGrade.EducationGrade1:
                    return "EducationGrade1";
                case EducationGrade.EducationGrade2:
                    return "EducationGrade2";
                case EducationGrade.EducationGrade3:
                    return "EducationGrade3";
                default:
                    return "EducationGrade1";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;

            switch (s)
            {
                case "EducationGrade1":
                    return EducationGrade.EducationGrade1;
                case "EducationGrade2":
                    return EducationGrade.EducationGrade2;
                case "EducationGrade3":
                    return EducationGrade.EducationGrade3;
                default:
                    return EducationGrade.EducationGrade1;
            }
        }

        #endregion
    }
}
