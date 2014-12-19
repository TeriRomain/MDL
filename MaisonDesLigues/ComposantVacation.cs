using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MaisonDesLigues
{
    public partial class ComposantVacation : Component
    {
        private Int32 _Top;


        private Int32 _Left;


        public ComposantVacation()
        {
            InitializeComponent();
            this._Top = this.groupBox1.Top;
            this._Left = this.groupBox1.Left;
        }

        public ComposantVacation(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public Int32 Top
        {
            get { return _Top; }
            set 
            { 
                _Top = value;
                this.groupBox1.Top = _Top;
            }
        }


        public Int32 Left
        {
            get { return _Left; }
            set 
            {
                _Left = value;
                this.groupBox1.Left = _Left;
            }
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }
    }
}
