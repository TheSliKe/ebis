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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ebis.Tabs
{
    /// <summary>
    /// Logique d'interaction pour JournalEntretien.xaml
    /// </summary>
    public partial class JournalEntretien : UserControl
    {
        public JournalEntretien()
        {
            InitializeComponent();
        }

        private void journalEntretienRecherche_TextChanged(object sender, TextChangedEventArgs e) { }
        private void journalEntretienList_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}
