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
        private Int32 _PositionGauche;
        private Int32 _PositionHaut;

        public ComposantVacation()
        {
            InitializeComponent();
        }

        public ComposantVacation(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public Int32 PositionHaut
        {
            get { return _PositionHaut; }
            set 
            {
                _PositionHaut = value;
                this.grpBoxVacationComposant.Top = this._PositionHaut;
            }
        }
        
        public Int32 PositionGauche
        {
            get { return _PositionGauche; }
            set 
            {
                _PositionGauche = value;
                this.grpBoxVacationComposant.Left = this._PositionGauche;
            }
        }
    }
}
