using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.ObjectModel;
using ComposantNuite;
using BaseDeDonnees;
using System.Globalization;


namespace MaisonDesLigues
{
    public partial class FrmPrincipale : Form
    {
        
        /// <summary>
        /// constructeur du formulaire
        /// </summary>
        public FrmPrincipale()
        {
            InitializeComponent();
        }
        private Bdd UneConnexion;
        private String TitreApplication;
        private String IdStatutSelectionne = "";
        
        /// <summary>
        /// création et ouverture d'une connexion vers la base de données sur le chargement du formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrincipale_Load(object sender, EventArgs e)
        {
            UneConnexion = ((FrmLogin)Owner).UneConnexion;
            TitreApplication = ((FrmLogin)Owner).TitreApplication;
            this.Text = TitreApplication;

            ComposantVacation test = new ComposantVacation(this.tabAjout.Container);
            test.PositionGauche = 600;
            test.PositionHaut = 50;

        }
        /// <summary>
        /// gestion de l'événement click du bouton quitter.
        /// Demande de confirmation avant de quitetr l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdQuitter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous quitter l'application ?", ConfigurationManager.AppSettings["TitreApplication"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                UneConnexion.FermerConnexion();
                Application.Exit();
            }
        }

        private void RadTypeParticipant_Changed(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "RadBenevole":
                    this.GererInscriptionBenevole();
                    break;
                case "RadLicencie":
                    this.GererInscriptionLicencie();
                    break;
                case "RadIntervenant":
                    this.GererInscriptionIntervenant();
                    break;
                default:
                    throw new Exception("Erreur interne à l'application");
            }
        }


        private void GererInscriptionLicencie()
        {
            GrpBenevole.Visible = false;
            GrpIntervenant.Visible = false;
            GrpLicencie.Visible = true;
            GrpLicencie.Left = 23;
            GrpLicencie.Top = 264;
            Utilitaire.CreerDesControles(this, UneConnexion, "VRESTAURATION01", "ChkRestoL_", PanRestoLicencie, "CheckBox", EnableBtnEnregistrerLicencie);
            Utilitaire.RemplirComboBox(UneConnexion, CmbAtelierLicencie, "VATELIER01");
            CmbAtelierLicencie.Text = "Choisir";
            
        }
        private void EnableBtnEnregistrerLicencie(object sender, EventArgs e)
        {
            BtnEnregistrerLicencie.Enabled = VerifBtnEnregistreLicencie();
        }
        private void RdbNuiteLicencie_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Name == "RdbNuiteLicencieOui")
            {
                PanNuiteLicencie.Visible = true;
                if (PanNuiteLicencie.Controls.Count == 0) // on charge les nuites possibles possibles et on les affiche
                {
                    //DataTable LesDateNuites = UneConnexion.ObtenirDonnesOracle("VDATENUITE01");
                    //foreach(Dat
                    Dictionary<Int16, String> LesNuites = UneConnexion.ObtenirDatesNuites();
                    int i = 0;
                    foreach (KeyValuePair<Int16, String> UneNuite in LesNuites)
                    {
                        ComposantNuite.ResaNuite unResaNuit = new ResaNuite(UneConnexion.ObtenirDonnesOracle("VHOTEL01"), (UneConnexion.ObtenirDonnesOracle("VCATEGORIECHAMBRE01")), UneNuite.Value, UneNuite.Key);
                        unResaNuit.Left = 5;
                        unResaNuit.Top = 5 + (24 * i++);
                        unResaNuit.Visible = true;
                        //unResaNuit.click += new System.EventHandler(ComposantNuite_StateChanged);
                        PanNuiteLicencie.Controls.Add(unResaNuit);
                    }

                }

            }
            else
            {
                PanNuiteLicencie.Visible = false;

            }
            BtnEnregistrerLicencie.Enabled = VerifBtnEnregistreLicencie();
        }
        private Boolean VerifBtnEnregistreLicencie()
        {
            return CmbAtelierIntervenant.Text != "Choisir" && TxtQualitéLicencie.Text!="" && TxtLicenceLicencie.Text!="";
        }
        //private void Vider_Champs()
        //{
        //    if (TabPrincipal.SelectedIndex == 0)
        //    {
        //        if (RadBenevole.Checked == true)
        //        {
        //            Collection<Control> MesControls = new Collection<Control>();
        //            foreach (Control Ctrl in PanelDispoBenevole.Controls)
        //            {
        //                if (Ctrl is CheckBox)
        //                {
        //                    MesControls.Add(Ctrl);
        //                }
        //            }
        //            foreach (Control Ctrl in GrpIdentite.Controls)
        //            {
        //                MesControls.Add(Ctrl);
        //            }
        //            foreach (Control Ctrl in GrpBenevole.Controls)
        //            {
        //                MesControls.Add(Ctrl);
        //            }

        //            foreach (Control Ctrl in MesControls)
        //            {
        //                if (Ctrl is TextBox || Ctrl is MaskedTextBox)
        //                    Ctrl.Text = string.Empty;
        //                else if (Ctrl is CheckBox)
        //                {
        //                    ((CheckBox)Ctrl).Checked = false;
        //                }
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// Procedure pour vider les champs de l'interface d'inscription 
        /// </summary>
        /// <param name="UnBoutonRadio">Selection du type: inscription, licencie ou benevole</param>
        /// <param name="UnGroupBox">Groupbox a vider</param>
        /// <param name="UnPanel">panel a vider,deschecker</param>
        private void Vider_Champs(GroupBox UnGroupBox)
        {
                    Collection<Control> MesControls = new Collection<Control>();
                    foreach (Control Ctrl in GrpIdentite.Controls)
                    {
                        MesControls.Add(Ctrl);
                    }
                    foreach (Control Ctrl in UnGroupBox.Controls)
                    {
                        MesControls.Add(Ctrl);
                        
                    }

                    foreach (Control Ctrl in MesControls)
                    {
                        if (Ctrl is TextBox || Ctrl is MaskedTextBox)
                            Ctrl.Text = string.Empty;
                        else if (Ctrl is CheckBox)
                        {
                            ((CheckBox)Ctrl).Checked = false;
                        }
                        else if (Ctrl is RadioButton)
                        {   
                            if (((RadioButton)Ctrl).Text == "Oui")
                            {
                                ((RadioButton)Ctrl).Checked = false;
                    }
                            if (((RadioButton)Ctrl).Text == "Non")
                            {
                                ((RadioButton)Ctrl).Checked = true;
                }
                           

            }
                                    
                    }
        }
        private void Vider_Panel(Panel UnPanel)
        {
            Collection<Control> MesControls = new Collection<Control>();
            foreach (Control Ctrl in UnPanel.Controls)
            {
                MesControls.Add(Ctrl);
        }
            foreach (Control Ctrl in MesControls)
            {
                if (Ctrl is CheckBox)
                {
                    ((CheckBox)Ctrl).Checked = false;
                }
                else if (Ctrl is RadioButton) {
                    ((RadioButton)Ctrl).Checked = false;
                }
                
                
            }
        }
        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie du complément d'inscription d'un intervenant.
        /// </summary>
        private void GererInscriptionIntervenant()
        {

            GrpBenevole.Visible = false;
            GrpIntervenant.Visible = true;
            PanFonctionIntervenant.Visible = true;
            GrpLicencie.Visible = false;
            GrpIntervenant.Left = 23;
            GrpIntervenant.Top = 264;
            Utilitaire.CreerDesControles(this, UneConnexion, "VSTATUT01", "Rad_", PanFonctionIntervenant, "RadioButton", this.rdbStatutIntervenant_StateChanged);
            Utilitaire.RemplirComboBox(UneConnexion, CmbAtelierIntervenant, "VATELIER01");

            CmbAtelierIntervenant.Text = "Choisir";

        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie des disponibilités des bénévoles.
        /// </summary>
        private void GererInscriptionBenevole()
        {

            GrpBenevole.Visible = true;
            GrpBenevole.Left = 23;
            GrpBenevole.Top = 264;
            GrpIntervenant.Visible = false;
            GrpLicencie.Visible = false;
            Utilitaire.CreerDesControles(this, UneConnexion, "VDATEBENEVOLAT01", "ChkDateB_", PanelDispoBenevole, "CheckBox", this.rdbStatutIntervenant_StateChanged);
            // on va tester si le controle à placer est de type CheckBox afin de lui placer un événement checked_changed
            // Ceci afin de désactiver les boutons si aucune case à cocher du container n'est cochée
            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox")
                {
                    CheckBox UneCheckBox = (CheckBox)UnControle;
                    UneCheckBox.CheckedChanged += new System.EventHandler(this.ChkDateBenevole_CheckedChanged);
                }
            }


        }
        /// <summary>
        /// permet d'appeler la méthode VerifBtnEnregistreIntervenant qui déterminera le statu du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbStatutIntervenant_StateChanged(object sender, EventArgs e)
        {
            // stocke dans un membre de niveau form l'identifiant du statut sélectionné (voir règle de nommage des noms des controles : prefixe_Id)
            this.IdStatutSelectionne = ((RadioButton)sender).Name.Split('_')[1];
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }
        /// <summary>
        /// Permet d'intercepter le click sur le bouton d'enregistrement d'un bénévole.
        /// Cetteméthode va appeler la méthode InscrireBenevole de la Bdd, après avoir mis en forme certains paramètres à envoyer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistreBenevole_Click(object sender, EventArgs e)
        {
            Collection<Int16> IdDatesSelectionnees = new Collection<Int16>();
            Int64? NumeroLicence;
            if (TxtLicenceBenevole.MaskCompleted)
            {
                NumeroLicence = System.Convert.ToInt64(TxtLicenceBenevole.Text);
            }
            else
            {
                NumeroLicence = null;
            }


            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox" && ((CheckBox)UnControle).Checked)
                {
                    /* Un name de controle est toujours formé come ceci : xxx_Id où id représente l'id dans la table
                     * Donc on splite la chaine et on récupére le deuxième élément qui correspond à l'id de l'élément sélectionné.
                     * on rajoute cet id dans la collection des id des dates sélectionnées
                        
                    */
                    IdDatesSelectionnees.Add(System.Convert.ToInt16((UnControle.Name.Split('_'))[1]));
                }
            }
            UneConnexion.InscrireBenevole(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToDateTime(TxtDateNaissance.Text), NumeroLicence, IdDatesSelectionnees);
            //Vider_Champs(RadBenevole,GrpBenevole,PanelDispoBenevole);
        }
        /// <summary>
        /// Cetet méthode teste les données saisies afin d'activer ou désactiver le bouton d'enregistrement d'un bénévole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkDateBenevole_CheckedChanged(object sender, EventArgs e)
        {
            BtnEnregistreBenevole.Enabled = (TxtLicenceBenevole.Text == "" || TxtLicenceBenevole.MaskCompleted) && TxtDateNaissance.MaskCompleted && Utilitaire.CompteChecked(PanelDispoBenevole) > 0;
        }
        /// <summary>
        /// Méthode qui permet d'afficher ou masquer le controle panel permettant la saisie des nuités d'un intervenant.
        /// S'il faut rendre visible le panel, on teste si les nuités possibles ont été chargés dans ce panel. Si non, on les charges 
        /// On charge ici autant de contrôles ResaNuit qu'il y a de nuits possibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdbNuiteIntervenant_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Name == "RdbNuiteIntervenantOui")
            {
                PanNuiteIntervenant.Visible = true;
                if (PanNuiteIntervenant.Controls.Count == 0) // on charge les nuites possibles possibles et on les affiche
                {
                    //DataTable LesDateNuites = UneConnexion.ObtenirDonnesOracle("VDATENUITE01");
                    //foreach(Dat
                    Dictionary<Int16, String> LesNuites = UneConnexion.ObtenirDatesNuites();
                    int i = 0;
                    foreach (KeyValuePair<Int16, String> UneNuite in LesNuites)
                    {
                        ComposantNuite.ResaNuite unResaNuit = new ResaNuite(UneConnexion.ObtenirDonnesOracle("VHOTEL01"), (UneConnexion.ObtenirDonnesOracle("VCATEGORIECHAMBRE01")), UneNuite.Value, UneNuite.Key);
                        unResaNuit.Left = 5;
                        unResaNuit.Top = 5 + (24 * i++);
                        unResaNuit.Visible = true;
                        //unResaNuit.click += new System.EventHandler(ComposantNuite_StateChanged);
                        PanNuiteIntervenant.Controls.Add(unResaNuit);
                    }

                }

            }
            else
            {
                PanNuiteIntervenant.Visible = false;

            }
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();

        }

        /// <summary>
        /// Cette procédure va appeler la procédure .... qui aura pour but d'enregistrer les éléments 
        /// de l'inscription d'un intervenant, avec éventuellment les nuités à prendre en compte        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistrerIntervenant_Click(object sender, EventArgs e)
        {
            try
            {
                if (RdbNuiteIntervenantOui.Checked)
                {
                    // inscription avec les nuitées
                    Collection<Int16> NuitsSelectionnes = new Collection<Int16>();
                    Collection<String> HotelsSelectionnes = new Collection<String>();
                    Collection<String> CategoriesSelectionnees = new Collection<string>();
                    foreach (Control UnControle in PanNuiteIntervenant.Controls)
                    {
                        if (UnControle.GetType().Name == "ResaNuite" && ((ResaNuite)UnControle).GetNuitSelectionnee())
                        {
                            // la nuité a été cochée, il faut donc envoyer l'hotel et la type de chambre à la procédure de la base qui va enregistrer le contenu hébergement 
                            //ContenuUnHebergement UnContenuUnHebergement= new ContenuUnHebergement();
                            CategoriesSelectionnees.Add(((ResaNuite)UnControle).GetTypeChambreSelectionnee());
                            HotelsSelectionnes.Add(((ResaNuite)UnControle).GetHotelSelectionne());
                            NuitsSelectionnes.Add(((ResaNuite)UnControle).IdNuite);
                         }

                    }
                    if (NuitsSelectionnes.Count == 0)
                    {
                        MessageBox.Show("Si vous avez sélectionné que l'intervenant avait des nuités\n in faut qu'au moins une nuit soit sélectionnée");
                    }
                    else
                    {
                        UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne, CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes);
                        MessageBox.Show("Inscription intervenant effectuée");
                    }
                }
                else
                { // inscription sans les nuitées
                    UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne);
                    MessageBox.Show("Inscription intervenant effectuée");
                    
                }
                Vider_Panel(PanNuiteIntervenant);
                Vider_Champs(GrpNuiteIntervenant);
                Vider_Champs(GrpIntervenant);
                Vider_Panel(PanFonctionIntervenant);

                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// Méthode privée testant le contrôle combo et la variable IdStatutSelectionne qui contient une valeur
        /// Cette méthode permetra ensuite de définir l'état du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <returns></returns>
        private Boolean VerifBtnEnregistreIntervenant()
        {
            return CmbAtelierIntervenant.Text != "Choisir" && this.IdStatutSelectionne.Length > 0;
        }
        /// <summary>
        /// Méthode permettant de définir le statut activé/désactivé du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAtelierIntervenant_TextChanged(object sender, EventArgs e)
        {
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }

        private void gererVacation(Boolean grand)
        {
            if (grand)
            {
                this.lblAtelierVacation.Visible = false;
                this.CmbBoxVacationAtelier.Visible = false;
                this.lblDateDbtVacation.Left = 0;
                this.lblDateDbtVacation.Top = 27;
                this.DateTimeDbtVacation.Left = 75;
                this.DateTimeDbtVacation.Top = 24;
                this.GrpBoxVacation.Width = 441;
                this.GrpBoxVacation.Height = 65;
                this.btnAjouterVacationAtelier.Visible = true;
                this.dtPickHeureDebutVacation.Top = 24;
                this.dtPickHeureDebutVacation.Left = 180;
                this.dtPickHeureFinVacation.Top = 24;
                this.dtPickHeureFinVacation.Left = 240;
            }
            else
            {
                this.btnAjouterVacationAtelier.Visible = false;
                this.lblAtelierVacation.Left = 28;
                this.lblAtelierVacation.Top = 37;
                this.lblAtelierVacation.Visible = true;
                this.CmbBoxVacationAtelier.Left = 75;
                this.CmbBoxVacationAtelier.Top = 34;
                this.CmbBoxVacationAtelier.Visible = true;
                this.lblDateDbtVacation.Left = 4;
                this.lblDateDbtVacation.Top = 67;
                this.DateTimeDbtVacation.Left = 80;
                this.DateTimeDbtVacation.Top = 64;
                this.dtPickHeureDebutVacation.Top = 64;
                this.dtPickHeureDebutVacation.Left = 180;
                this.dtPickHeureFinVacation.Top = 64;
                this.dtPickHeureFinVacation.Left = 240;
                this.GrpBoxVacation.Width = 453;
                this.GrpBoxVacation.Height = 142;
            }
        }

        /// <summary>
        /// fonction qui modifie dynamiquement la taille de grpBoxAddTheme
        /// </summary>
        /// <param name="grand"></param>

        private void gererTheme(Boolean grand)
        {
            if (grand)
            {
                this.LblThemeAtelier.Visible = false;
                this.LblThemeLibelle.Left = 25;
                this.LblThemeLibelle.Top = 25;
                this.cmbBoxThemeAtelier.Visible = false;
                this.txtBoxAddThemeLibelle.Top = 22;
                this.txtBoxAddThemeLibelle.Left = 77;
                this.grpBoxAddTheme.Width = 441;
                this.grpBoxAddTheme.Height = 64;
                this.btnAddThemeEnregistre.Visible = false;
                this.btnAjouterThemeAtelier.Visible = true;
            }
            else
            {
                this.btnAjouterThemeAtelier.Visible = false;
                this.LblThemeAtelier.Left = 34;
                this.LblThemeAtelier.Top = 37;
                this.LblThemeAtelier.Visible = true;
                this.LblThemeLibelle.Left = 242;
                this.LblThemeLibelle.Top = 37;
                this.cmbBoxThemeAtelier.Top = 34;
                this.cmbBoxThemeAtelier.Left = 85;
                this.cmbBoxThemeAtelier.Visible = true;
                this.txtBoxAddThemeLibelle.Top = 34;
                this.txtBoxAddThemeLibelle.Left = 308;
                this.grpBoxAddTheme.Width = 453;
                this.grpBoxAddTheme.Height = 110;
                this.btnAddThemeEnregistre.Visible = true;
            }
        }

        /// <summary>
        /// Methode qui crée dynamiquement l'interface d'ajout d'un atelier
        /// </summary>
        private void GererInterfaceAtelier()
        {
            this.grpBoxAtelier.Controls.Add(this.grpBoxAddTheme);
            this.gererTheme(true);
            this.grpBoxAtelier.Controls["grpBoxAddTheme"].Left = 6;
            this.grpBoxAtelier.Controls["grpBoxAddTheme"].Top = 88;
            this.grpBoxAddTheme.Visible = true;

              
            this.grpBoxAtelier.Controls.Add(this.GrpBoxVacation);
            this.grpBoxAtelier.Controls["GrpBoxVacation"].Visible = true;
            this.grpBoxAtelier.Controls["GrpBoxVacation"].Left = 6;
            this.grpBoxAtelier.Controls["GrpBoxVacation"].Top = 158;
            this.gererVacation(true);
        }

        private void rdrbtnAtelier_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdrbtnAtelier.Checked)
            {
                this.grpBoxAtelier.Visible = true;
                this.GererInterfaceAtelier();
            }
            else
            {
                this.tabAjout.Controls.Add(this.grpBoxAtelier.Controls["grpBoxAddTheme"]);
                this.tabAjout.Controls.Add(this.grpBoxAtelier.Controls["grpBoxVacation"]);
                this.grpBoxAtelier.Visible = false;
                this.GrpBoxVacation.Visible = false;
                this.grpBoxAddTheme.Top = 596;
                this.grpBoxAddTheme.Left = 6;
            }
        }


        private void rdrBtnTheme_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdrBtnTheme.Checked)
            {
                this.gererTheme(false);
                this.grpBoxAddTheme.Top = 71;
                this.grpBoxAddTheme.Left = 23;
                this.grpBoxAddTheme.Visible = true;
                this.btnAjouterThemeAtelier.Visible = false;
                Utilitaire.RemplirComboBox(this.UneConnexion, this.cmbBoxThemeAtelier, "VATELIER01");
                this.cmbBoxThemeAtelier.Text = "Choisir";
                
            }
            else
            {
                this.btnAjouterThemeAtelier.Visible = true;
                this.grpBoxAddTheme.Visible = false;
                this.grpBoxAddTheme.Top = 187;
                this.grpBoxAddTheme.Left = 453;
            }
        }

        private void rdrBtnVacation_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdrBtnVacation.Checked)
            {
                this.gererVacation(false);
                this.GrpBoxVacation.Visible = true;
                this.GrpBoxVacation.Top = 71;
                this.GrpBoxVacation.Left = 23;
                Utilitaire.RemplirComboBox(this.UneConnexion, this.CmbBoxVacationAtelier, "VATELIER01");
                this.CmbBoxVacationAtelier.Text = "Choisir";
            }
            else
            {
                this.GrpBoxVacation.Visible = false;
                this.GrpBoxVacation.Top = 187;
                this.GrpBoxVacation.Left = 453;
            }
        }

        private void TabInscription_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Rad_NuiteOui_CheckedChanged(object sender, EventArgs e)
        {
            

        }

        private void TabInscription_Click_1(object sender, EventArgs e)
        {

        }

        private void dtPickHeureDebutVacation_ValueChanged(object sender, EventArgs e)
        {

        }
        
        private void btnAddThemeEnregistre_Click(object sender, EventArgs e)
        {
            try
            {
                this.UneConnexion.AjoutTheme(Convert.ToInt16(this.cmbBoxThemeAtelier.SelectedValue), this.txtBoxAddThemeLibelle.Text);

                MessageBox.Show("theme ajouté a l'atelier " + this.cmbBoxThemeAtelier.Text);

                this.Vider_Champs(this.grpBoxAddTheme);
                this.cmbBoxThemeAtelier.Text = "Choisir";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabAjout_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// methode qui fait appel a la fonction AjoutVacation pour ajouter une Vacation a un Atelier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistreVacation_Click(object sender, EventArgs e)
        {
            try
            {
                CultureInfo provider = new CultureInfo("fr-FR");
                string dateStringDebut = this.DateTimeDbtVacation.Text + " " + this.dtPickHeureDebutVacation.Text;
                string dateStringFin = this.DateTimeDbtVacation.Text + " " + this.dtPickHeureFinVacation.Text;
                MessageBox.Show(dateStringDebut + "\n" + dateStringFin);

                string format = "g";
                DateTime resultDebut = DateTime.ParseExact(dateStringDebut, format, provider);
                DateTime resultFin = DateTime.ParseExact(dateStringFin, format, provider);
                this.UneConnexion.AjoutVacation(Convert.ToInt16(this.CmbBoxVacationAtelier.SelectedValue), resultDebut, resultFin);

                MessageBox.Show("theme ajouté a l'atelier " + this.cmbBoxThemeAtelier.Text);

                this.Vider_Champs(this.grpBoxAddTheme);
                this.cmbBoxThemeAtelier.Text = "Choisir";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RdbNuiteLicencieNon_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RdbNuiteLicencieOui_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnEnregistrerLicencie_Click(object sender, EventArgs e)
        {
            try
            {
                if (RdbNuiteIntervenantOui.Checked)
                {
                    // inscription avec les nuitées
                    Collection<Int16> NuitsSelectionnes = new Collection<Int16>();
                    Collection<String> HotelsSelectionnes = new Collection<String>();
                    Collection<String> CategoriesSelectionnees = new Collection<string>();
                    Collection<String> RestaurationSelectionnes = new Collection<String>();
                    foreach (Control UnControle in PanRestoLicencie.Controls)
                    {
                        RestaurationSelectionnes.Add(System.Convert.ToString((CheckBox)UnControle));
                    }
                    foreach (Control UnControle in PanNuiteLicencie.Controls)
                    {
                        if (UnControle.GetType().Name == "ResaNuite" && ((ResaNuite)UnControle).GetNuitSelectionnee())
                        {
                            // la nuité a été cochée, il faut donc envoyer l'hotel et la type de chambre à la procédure de la base qui va enregistrer le contenu hébergement 
                            //ContenuUnHebergement UnContenuUnHebergement= new ContenuUnHebergement();
                            CategoriesSelectionnees.Add(((ResaNuite)UnControle).GetTypeChambreSelectionnee());
                            HotelsSelectionnes.Add(((ResaNuite)UnControle).GetHotelSelectionne());
                            NuitsSelectionnes.Add(((ResaNuite)UnControle).IdNuite);
                        }

                    }
                    if (NuitsSelectionnes.Count == 0)
                    {
                        MessageBox.Show("Si vous avez sélectionné que l'intervenant avait des nuités\n in faut qu'au moins une nuit soit sélectionnée");
                    }
                    else if (RestaurationSelectionnes.Count > 0)
                    {
                        UneConnexion.InscrireLicencie(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt64(TxtLicenceLicencie.Text), System.Convert.ToInt32(TxtQualitéLicencie.Text), System.Convert.ToInt16(CmbAtelierLicencie.SelectedValue), CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes, RestaurationSelectionnes);
                    }
                    else
                    {
                        UneConnexion.InscrireLicencie(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt64(TxtLicenceLicencie.Text), System.Convert.ToInt32(TxtQualitéLicencie.Text), System.Convert.ToInt16(CmbAtelierLicencie.SelectedValue), CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes);
                        MessageBox.Show("Inscription Licencié effectuée");
                    }
                }
                else
                { // inscription sans les nuitées
                    UneConnexion.InscrireLicencie(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null,  System.Convert.ToInt64(TxtLicenceLicencie.Text), System.Convert.ToInt32(TxtQualitéLicencie.Text),System.Convert.ToInt16(CmbAtelierLicencie.SelectedValue));
                    MessageBox.Show("Inscription Licencié effectuée");

                }
                Vider_Panel(PanNuiteLicencie);
                Vider_Champs(GrpNuiteLicencie);
                Vider_Champs(GrpLicencie);
                Vider_Panel(PanRestoLicencie);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ChkDateBenevole_CheckedChanged(object sender, KeyEventArgs e)
        //{

        //}


    }
}

