using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FifthHomework
{
    // описываем класс Счетчик, который включает диапазон( MinNum, MAxNum), 
    // текущее значение (PresValue), кол-во итераций +-(NumIter) и info() - вывод этих данных на экран
    public class Counter
    {
        public Counter(int _minnum, int _maxnum, int _presvalue, int _pluspush, int _minuspush)
        {
           MinNum = _minnum;
           MaxNum = _maxnum;
           PresValue = _presvalue;
            PlusPush = _pluspush;
            MinusPush = _minuspush;
        }

        public int _minnum;
        public int MinNum
        {
            set
            { 
            _minnum = value;
            }

            get
            {
                return _minnum;
            }
        }

        public int _maxnum;
        public int MaxNum
        {
            set
            {
                _maxnum = value;
            }

            get
            {
                return _maxnum;
            }
        }

        public int _pluspush;
        public int PlusPush
        {
            set
            {
                _pluspush = value;
            }

            get
            {
                return _pluspush;
            }
        }

        public int _minuspush;
        public int MinusPush
        {
            set
            {
                _minuspush = value;
            }

            get
            {
                return _minuspush;
            }
        }

        public int _presvalue;
        public int PresValue
        {
            set
            {
                _presvalue = value;
            }

            get
            {
                return _presvalue;
            }
        }

        // Вывод информации о текущем состоянии счётчика
        public int Info()
        {
            MessageBox.Show(string.Format("Текущее значение счетчика: {0}\n В диапазоне [{1}, {2}] \n Кол-во нажатий на плюс: {3} \n Кол-во нажатий на минус: {4}", _presvalue, _minnum, _maxnum, _pluspush, _minuspush));
            return 0;
        } 
    }
        /// <summary>
        /// Логика взаимодействия для MainWindow.xaml
        /// </summary>

        public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Counter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ManualCounter_Click(object sender, RoutedEventArgs e)
        {
            // проверим, ввел ли пользователь
            if(String.IsNullOrEmpty(tb_minDia.Text) || String.IsNullOrEmpty(tb_maxDia.Text))
            {
                MessageBox.Show("Вы не ввели диапазон");
                return;
            }

            // проверяем, ввёл ли пользователь число в диапазон
            try
            {
                int Test1 = int.Parse(tb_minDia.Text);
                int Test2 = int.Parse(tb_maxDia.Text);
            }

            catch (FormatException)
            {
                MessageBox.Show("Диапазон имеет числовой формат!");
                return;
            }

            if (int.Parse(tb_minDia.Text)  > int.Parse(tb_maxDia.Text))
            {
                MessageBox.Show("Минимум диапазона не может быть больше максимума!\nПоменяйте минимум или увиличьте максимум");
                return;
            }

            MessageBox.Show("Теперь вы можете  увеличивать или уменьшать значение счетчика");
            Random Rand = new Random();
            Counter.Text = Rand.Next(int.Parse(tb_minDia.Text), int.Parse(tb_maxDia.Text)).ToString();
            Minus.IsEnabled = true; Plus.IsEnabled = true;
            Counter.IsEnabled = true; Counter.IsReadOnly = true;
            tb_minDia.IsReadOnly = true; tb_maxDia.IsReadOnly = true;
            ManualCounter.IsEnabled = false; AutoInfill.IsEnabled = false;
        }

        private void MaxNum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AutoInfill_Click(object sender, RoutedEventArgs e)
        {
            Random Rand = new Random();
            tb_minDia.Text = Rand.Next(-10000, 10000).ToString();
            tb_maxDia.Text = Rand.Next(int.Parse(tb_minDia.Text), 10001).ToString();
            Counter.Text = Rand.Next(int.Parse(tb_minDia.Text), int.Parse(tb_maxDia.Text)).ToString();
            MessageBox.Show("Теперь вы можете  увеличивать или уменьшать значение счетчика");
            Minus.IsEnabled = true; Plus.IsEnabled = true;
            Counter.IsEnabled = true; Counter.IsReadOnly = true;
            tb_minDia.IsReadOnly = true; tb_maxDia.IsReadOnly = true;
            ManualCounter.IsEnabled = false; AutoInfill.IsEnabled = false;
        }

        //  кнопка увеличенивает число
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            // проверка, входит ли число в диапазон
            if(int.Parse(tb_maxDia.Text) <= int.Parse(Counter.Text))
            {
                MessageBox.Show("Вы достигли предела диапазона!");
                return;
            }
            else
            {
                Counter.Text = (int.Parse(Counter.Text) + 1).ToString();
                PlusPush.Text = (int.Parse(PlusPush.Text) + 1).ToString(); 
            }
        }

        // кнопка уменьшает число
        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            // проверка, входит ли число в диапазон
            if (int.Parse(tb_minDia.Text) >= int.Parse(Counter.Text))
            {
                MessageBox.Show("Вы достигли предела диапазона!");
                return;
            }
            else
            {
                Counter.Text = (int.Parse(Counter.Text) - 1).ToString();
                MinusPush.Text = (int.Parse(MinusPush.Text) + 1).ToString();
            }
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            // Создаем объект класса и выводим информацию о нём
            Counter NewCounter = new Counter(int.Parse(tb_minDia.Text), int.Parse(tb_maxDia.Text), int.Parse(Counter.Text), int.Parse(PlusPush.Text), int.Parse(MinusPush.Text));
            NewCounter.Info();
            // Возвражаемся к начальному состоянию
            Minus.IsEnabled = false; Plus.IsEnabled = false;
            Counter.IsEnabled = false; Counter.IsReadOnly = true;
            Counter.Text = ""; tb_minDia.Text = ""; tb_maxDia.Text = "";
            tb_minDia.IsReadOnly = true; tb_maxDia.IsReadOnly = true;
            ManualCounter.IsEnabled = true; AutoInfill.IsEnabled = true;
            MinusPush.Text = "0".ToString(); PlusPush.Text = "0".ToString();
        }

        private void MinusPush_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PlusPush_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
