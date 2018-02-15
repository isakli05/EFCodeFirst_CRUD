using EFCodeFirst.Model.Context;
using EFCodeFirst.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFCodeFirst.Model.Enum;

namespace EFCodeFirst_CRUD
{
    public partial class Urun : Form
    {
        public Urun()
        {
            InitializeComponent();
        }
        ProjectContext db = new ProjectContext();
        void ProductList()
        {
            lvUrunler.Items.Clear();
            //Statü kullanılmadıysa where gerekli değildir.
            foreach (Product item in db.Products.Where(x=>x.Status==Status.Active||x.Status==Status.Updated).ToList())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.ID.ToString();
                lvi.SubItems.Add(item.Name);
                lvi.SubItems.Add(item.UnitsInStock.ToString());
                lvi.SubItems.Add(item.Price.ToString());
                lvi.SubItems.Add(item.Category.Name);
                lvi.SubItems.Add(item.Quantity);
                lvi.Tag = item;

                lvUrunler.Items.Add(lvi);
            }
        }
        void CategoryList()
        {
            //Where bölümü statü kullanılmadıysa gereksizdir.
            cmbKategoriler.DataSource = db.Categories.Where(x=>x.Status==Status.Active||x.Status==Status.Updated).ToList();
            cmbKategoriler.DisplayMember = "Name";
            cmbKategoriler.ValueMember = "Id";
            cmbKategoriler.SelectedIndex = -1;
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product silinecekUrun = new Product();
            silinecekUrun = (Product)lvUrunler.SelectedItems[0].Tag;

            //Eğer statü kullanmıyorsak remove ile silme yapılmalıdır.(2 alt satırda.)
            silinecekUrun.Status = EFCodeFirst.Model.Enum.Status.Deleted;
            db.Entry(db.Products.Find(silinecekUrun.ID)).CurrentValues.SetValues(silinecekUrun);

            //Statü yerine direk sileceksek aşağıdaki kod gereklidir.
            //db.Products.Remove((Product)lvUrunler.SelectedItems[0].Tag);

            db.SaveChanges();
            ProductList();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Product eklenecekUrun = new Product();
            eklenecekUrun.CategoryID = (int)cmbKategoriler.SelectedValue;
            eklenecekUrun.Name = txtAd.Text;
            eklenecekUrun.Price = nmrFiyat.Value;
            eklenecekUrun.UnitsInStock = (short)nmrStokAdet.Value;
            eklenecekUrun.Quantity = txtBirim.Text;

            //Statü kullanılmadıysa yapılmayacak.
            eklenecekUrun.Status = Status.Active;

            db.Products.Add(eklenecekUrun);
            db.SaveChanges();

            ProductList();

            txtAd.Text = string.Empty;
            txtBirim.Text = string.Empty;
            nmrFiyat.Value = nmrFiyat.Minimum;
            nmrStokAdet.Value = nmrStokAdet.Minimum;
            cmbKategoriler.SelectedIndex = -1;
        }
        Product guncellenecek;
        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guncellenecek = (Product)lvUrunler.SelectedItems[0].Tag;
            txtAd.Text = guncellenecek.Name;
            nmrFiyat.Value = guncellenecek.Price;
            nmrStokAdet.Value = guncellenecek.UnitsInStock;
            cmbKategoriler.SelectedValue = guncellenecek.CategoryID;
            txtBirim.Text = guncellenecek.Quantity;
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            guncellenecek.Name = txtAd.Text;
            guncellenecek.Price = nmrFiyat.Value;
            guncellenecek.UnitsInStock = (short)nmrStokAdet.Value;
            guncellenecek.CategoryID = (int)cmbKategoriler.SelectedValue;
            guncellenecek.Quantity = txtBirim.Text;

            //Statü eklenmediyse yapılmayacak.
            guncellenecek.Status = Status.Updated;

            db.Entry(db.Products.Find(guncellenecek.ID)).CurrentValues.SetValues(guncellenecek);
            db.SaveChanges();

            txtAd.Text = string.Empty;
            txtBirim.Text = string.Empty;
            nmrFiyat.Value = nmrFiyat.Minimum;
            nmrStokAdet.Value = nmrStokAdet.Minimum;
            cmbKategoriler.SelectedIndex = -1;

            ProductList();
        }

        private void Urun_Load(object sender, EventArgs e)
        {
            ProductList();
            CategoryList();
        }

        private void lvUrunler_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lvUrunler.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }
    }
}
