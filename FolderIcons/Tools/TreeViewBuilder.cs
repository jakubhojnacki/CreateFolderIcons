using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Tree view builder class
	/// </summary>
	class TreeViewBuilder
	{

		#region General properties

		/// <summary>
		/// The tree view
		/// </summary>
		public TreeView TreeView { get; set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pTreeView">Tree view to be built</param>
		public TreeViewBuilder(TreeView pTreeView)
		{
			this.TreeView = pTreeView;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Clearing items
		/// </summary>
		public void ClearItems()
		{
			this.TreeView.Items.Clear();
		}

		/// <summary>
		/// Adding a new item to root
		/// </summary>
		/// <param name="pHeader">Header</param>
		/// <param name="pImage">Image</param>
		/// <returns>Item added</returns>
		public TreeViewItem AddItemToRoot(string pHeader, string pImage)
		{
			TreeViewItem lItem = this.BuildItem(pHeader, pImage);
			this.TreeView.Items.Add(lItem);
			return lItem;
		}

		/// <summary>
		/// Adding a new item to specified item
		/// </summary>
		/// <param name="pParent">Parent item</param>
		/// <param name="pHeader">Header</param>
		/// <param name="pImage">Image</param>
		/// <returns>Item added</returns>
		public TreeViewItem AddItem(TreeViewItem pParent, string pHeader, string pImage)
		{
			TreeViewItem lItem = this.BuildItem(pHeader, pImage);
			pParent.Items.Add(lItem);
			return lItem;
		}

		/// <summary>
		/// Setting item header
		/// </summary>
		/// <param name="pItem">The item</param>
		/// <param name="pHeader">Header to be set</param>
		public void SetItemHeader(TreeViewItem pItem, string pHeader)
		{
			bool lHeaderSet = false;
			if (pItem.Header is StackPanel)
			{
				StackPanel lItemStackPanel = (StackPanel)pItem.Header;
				if (lItemStackPanel.Children.Count >= 2)
					if (lItemStackPanel.Children[1] is Label)
					{
						Label lItemLabel = (Label)lItemStackPanel.Children[1];
						lItemLabel.Content = pHeader;
						lHeaderSet = true;
					}
			}
			if (!lHeaderSet)
				pItem.Header = pHeader;
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Building an item
		/// </summary>
		/// <param name="pHeader">Item's header</param>
		/// <param name="pImage">Image</param>
		/// <returns>Item added</returns>
		protected TreeViewItem BuildItem(string pHeader, string pImage)
		{
			TreeViewItem lItem = new TreeViewItem();
			StackPanel lItemStackPanel = new StackPanel();
			lItemStackPanel.Orientation = Orientation.Horizontal;
			lItemStackPanel.Children.Add(new Image() { Source = new BitmapImage(new Uri(pImage, UriKind.Relative)) });
			lItemStackPanel.Children.Add(new Label() { Content = pHeader });
			lItem.Header = lItemStackPanel;
			return lItem;  
		}

		#endregion

	}

}
