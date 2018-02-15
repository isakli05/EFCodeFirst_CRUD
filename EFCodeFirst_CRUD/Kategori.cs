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
    public partial class Kategori : Form
    {
        public Kategori()
        {
            InitializeComponent();
        }

        ProjectContext db = new ProjectContext();
        void CategoryList()
        {
            lvKategoriler.Items.Clear();
            foreach (Category item in db.Categories.Where(x=>x.Status==Status.Active||x.Status==Status.Updated).ToList())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.ID.ToString();
                lvi.SubItems.Add(item.Name);
                lvi.SubItems.Add(item.Description);
                lvi.Tag = item;
                lvKategoriler.Items.Add(lvi);
            }
        }



        private void btnKategoriEkle_Click(object sender, EventArgs e)
        {
            Category eklenecekKategori = new Category();
            eklenecekKategori.Name = txtKategoriAd.Text;
            eklenecekKategori.Description = txtAciklama.Text;
            //Eğer statü kullanılmadıysa aşağıdaki satıra gerek yoktur.
            eklenecekKategori.Status = Status.Active;

            db.Categories.Add(eklenecekKategori);
            db.SaveChanges();

            txtAciklama.Text = string.Empty;
            txtKategoriAd.Text = string.Empty;

            CategoryList();
        }

        private void Kategori_Load(object sender, EventArgs e)
        {
            CategoryList();
        }



        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category silinecekKategori = new Category();
            silinecekKategori = (Category)lvKategoriler.SelectedItems[0].Tag;
            //Gerçekten silinmesini engellemek için. Statü kulalnımadıysa gerek yoktur.
            silinecekKategori.Status = Status.Deleted;
            
            //Gerçek silme işlemi.(Statü yok ise)
            //db.Categories.Remove((Category)lvKategoriler.SelectedItems[0].Tag);

            db.Entry(db.Categories.Find(silinecekKategori.ID)).CurrentValues.SetValues(silinecekKategori);
            db.SaveChanges();
            CategoryList();
        }

        private void lvKategoriler_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lvKategoriler.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }
        Category guncellenecek;
        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guncellenecek = (Category)lvKategoriler.SelectedItems[0].Tag;
            txtKategoriAd.Text = guncellenecek.Name;
            txtAciklama.Text = guncellenecek.Description;
        }

        private void btnKategoriGuncelle_Click(object sender, EventArgs e)
        {
            guncellenecek.Name = txtKategoriAd.Text;
            guncellenecek.Description = txtAciklama.Text;

            //Statü yoksa yapılmayacak.
            guncellenecek.Status = Status.Updated;

            db.Entry(db.Categories.Find(guncellenecek.ID)).CurrentValues.SetValues(guncellenecek);
            db.SaveChanges();
            CategoryList();
            txtKategoriAd.Text = string.Empty;
            txtAciklama.Text = string.Empty;
        }

        private void btnUruneGit_Click(object sender, EventArgs e)
        {
            Urun frm = new Urun();
            frm.Show();
        }
    }
}
