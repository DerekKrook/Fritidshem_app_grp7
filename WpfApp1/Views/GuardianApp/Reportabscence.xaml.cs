﻿using System;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Reportabscence.xaml
    /// </summary>
    public partial class Reportabscence : Window
    {
        List<Child> children = new List<Child>();

        public Reportabscence()
        {
            InitializeComponent();

            DataBinding();
        }

        //Tänk en formulär med flera comboboxar där man väljer barn, frånvaro anlednning, datum och sen en textbox med kommentar :D

        public void DataBinding()
        {
            children = DbOperations.GetChildrenOfGuardian();

            comboBoxChildren.ItemsSource = children;
            comboBoxChildren.DisplayMemberPath = "Fullinformation";

            comboBoxChildren.SelectedIndex = 0;
        }

        private void ComboBoxChildren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxChildren.SelectedItem != null)
            {
                Activechild.Setactivechild((Child)comboBoxChildren.SelectedItem);
            }
        }
    }
}