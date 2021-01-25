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
using System.Windows.Shapes;

namespace JHJ.FolderIcons
{
	/// <summary>
	/// Interaction logic for AboutWindow.xaml
	/// </summary>
	public partial class AboutWindow : Window
	{

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public AboutWindow()
		{
			this.InitializeComponent();
			this.UpdateLabels();
		}

		/// <summary>
		/// Updating labels
		/// </summary>
		protected void UpdateLabels()
		{
			this.AssemblyTitleLabel.Content = AssemblyToolkit.AssemblyTitle;
			this.AssemblyVersionLabel.Content = AssemblyToolkit.AssemblyVersion;
			this.AssemblyDescriptionLabel.Content = AssemblyToolkit.AssemblyDescription;
			this.AssemblyCompanyLabel.Content = AssemblyToolkit.AssemblyCompany;
			this.AssemblyCopyrightLabel.Content = AssemblyToolkit.AssemblyCopyright;
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// On close button click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void CloseButtonClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		#endregion

	}
}
