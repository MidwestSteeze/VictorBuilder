namespace VictorBuilder
{
    partial class CreateItemForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateItemForm));
            this.gbWeapon = new System.Windows.Forms.GroupBox();
            this.btnCreateWeapon = new System.Windows.Forms.Button();
            this.tcWeaponMods = new System.Windows.Forms.TabControl();
            this.tpAffixes = new System.Windows.Forms.TabPage();
            this.lstThirdAffix = new System.Windows.Forms.ListBox();
            this.lblThirdAffix = new System.Windows.Forms.Label();
            this.lstSuffixes = new System.Windows.Forms.ListBox();
            this.lstPrefixes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.tpLegendaries = new System.Windows.Forms.TabPage();
            this.lblLegendaryName = new System.Windows.Forms.Label();
            this.lstLegendaries = new System.Windows.Forms.ListBox();
            this.lblWeaponRarity = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cboWeaponRarity = new System.Windows.Forms.ComboBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.gbCard = new System.Windows.Forms.GroupBox();
            this.btnCreateCard = new System.Windows.Forms.Button();
            this.lstCardWickedMods = new System.Windows.Forms.ListBox();
            this.lstCardDivineMods = new System.Windows.Forms.ListBox();
            this.lblCardDivineMods = new System.Windows.Forms.Label();
            this.lblCardWickedMods = new System.Windows.Forms.Label();
            this.lstCards = new System.Windows.Forms.ListBox();
            this.lblCards = new System.Windows.Forms.Label();
            this.lblDivineWicked = new System.Windows.Forms.Label();
            this.cboDivineWicked = new System.Windows.Forms.ComboBox();
            this.lblCardRarity = new System.Windows.Forms.Label();
            this.cboCardRarity = new System.Windows.Forms.ComboBox();
            this.weaponsBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.weaponsBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.weaponsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemsDataSet = new VictorBuilder.ItemsDataSet();
            this.weaponsTableAdapter = new VictorBuilder.ItemsDataSetTableAdapters.WeaponsTableAdapter();
            this.tableAdapterManager = new VictorBuilder.ItemsDataSetTableAdapters.TableAdapterManager();
            this.gbWeapon.SuspendLayout();
            this.tcWeaponMods.SuspendLayout();
            this.tpAffixes.SuspendLayout();
            this.tpLegendaries.SuspendLayout();
            this.gbCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponsBindingNavigator)).BeginInit();
            this.weaponsBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // gbWeapon
            // 
            this.gbWeapon.Controls.Add(this.btnCreateWeapon);
            this.gbWeapon.Controls.Add(this.tcWeaponMods);
            this.gbWeapon.Controls.Add(this.lblWeaponRarity);
            this.gbWeapon.Controls.Add(this.lblType);
            this.gbWeapon.Controls.Add(this.cboWeaponRarity);
            this.gbWeapon.Controls.Add(this.cboType);
            this.gbWeapon.Location = new System.Drawing.Point(12, 12);
            this.gbWeapon.Name = "gbWeapon";
            this.gbWeapon.Size = new System.Drawing.Size(480, 542);
            this.gbWeapon.TabIndex = 0;
            this.gbWeapon.TabStop = false;
            this.gbWeapon.Text = "Weapon";
            // 
            // btnCreateWeapon
            // 
            this.btnCreateWeapon.Location = new System.Drawing.Point(45, 493);
            this.btnCreateWeapon.Name = "btnCreateWeapon";
            this.btnCreateWeapon.Size = new System.Drawing.Size(75, 23);
            this.btnCreateWeapon.TabIndex = 8;
            this.btnCreateWeapon.Text = "Create";
            this.btnCreateWeapon.UseVisualStyleBackColor = true;
            this.btnCreateWeapon.Click += new System.EventHandler(this.btnCreateWeapon_Click);
            // 
            // tcWeaponMods
            // 
            this.tcWeaponMods.Controls.Add(this.tpAffixes);
            this.tcWeaponMods.Controls.Add(this.tpLegendaries);
            this.tcWeaponMods.Enabled = false;
            this.tcWeaponMods.Location = new System.Drawing.Point(45, 88);
            this.tcWeaponMods.Name = "tcWeaponMods";
            this.tcWeaponMods.SelectedIndex = 0;
            this.tcWeaponMods.Size = new System.Drawing.Size(429, 399);
            this.tcWeaponMods.TabIndex = 7;
            // 
            // tpAffixes
            // 
            this.tpAffixes.AutoScroll = true;
            this.tpAffixes.Controls.Add(this.lstThirdAffix);
            this.tpAffixes.Controls.Add(this.lblThirdAffix);
            this.tpAffixes.Controls.Add(this.lstSuffixes);
            this.tpAffixes.Controls.Add(this.lstPrefixes);
            this.tpAffixes.Controls.Add(this.label1);
            this.tpAffixes.Controls.Add(this.lblSuffix);
            this.tpAffixes.Location = new System.Drawing.Point(4, 22);
            this.tpAffixes.Name = "tpAffixes";
            this.tpAffixes.Padding = new System.Windows.Forms.Padding(3);
            this.tpAffixes.Size = new System.Drawing.Size(421, 373);
            this.tpAffixes.TabIndex = 0;
            this.tpAffixes.Text = "Affixes";
            this.tpAffixes.UseVisualStyleBackColor = true;
            // 
            // lstThirdAffix
            // 
            this.lstThirdAffix.FormattingEnabled = true;
            this.lstThirdAffix.Location = new System.Drawing.Point(78, 231);
            this.lstThirdAffix.Name = "lstThirdAffix";
            this.lstThirdAffix.Size = new System.Drawing.Size(337, 95);
            this.lstThirdAffix.TabIndex = 13;
            // 
            // lblThirdAffix
            // 
            this.lblThirdAffix.AutoSize = true;
            this.lblThirdAffix.Location = new System.Drawing.Point(6, 231);
            this.lblThirdAffix.Name = "lblThirdAffix";
            this.lblThirdAffix.Size = new System.Drawing.Size(57, 13);
            this.lblThirdAffix.TabIndex = 12;
            this.lblThirdAffix.Text = "Third Affix:";
            // 
            // lstSuffixes
            // 
            this.lstSuffixes.FormattingEnabled = true;
            this.lstSuffixes.Location = new System.Drawing.Point(78, 130);
            this.lstSuffixes.Name = "lstSuffixes";
            this.lstSuffixes.Size = new System.Drawing.Size(337, 95);
            this.lstSuffixes.TabIndex = 11;
            this.lstSuffixes.SelectedIndexChanged += new System.EventHandler(this.lstSuffixes_SelectedIndexChanged);
            // 
            // lstPrefixes
            // 
            this.lstPrefixes.FormattingEnabled = true;
            this.lstPrefixes.Location = new System.Drawing.Point(77, 27);
            this.lstPrefixes.Name = "lstPrefixes";
            this.lstPrefixes.Size = new System.Drawing.Size(338, 95);
            this.lstPrefixes.TabIndex = 10;
            this.lstPrefixes.SelectedIndexChanged += new System.EventHandler(this.lstPrefixes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Prefix:";
            // 
            // lblSuffix
            // 
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Location = new System.Drawing.Point(6, 130);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(36, 13);
            this.lblSuffix.TabIndex = 9;
            this.lblSuffix.Text = "Suffix:";
            // 
            // tpLegendaries
            // 
            this.tpLegendaries.Controls.Add(this.lblLegendaryName);
            this.tpLegendaries.Controls.Add(this.lstLegendaries);
            this.tpLegendaries.Location = new System.Drawing.Point(4, 22);
            this.tpLegendaries.Name = "tpLegendaries";
            this.tpLegendaries.Padding = new System.Windows.Forms.Padding(3);
            this.tpLegendaries.Size = new System.Drawing.Size(421, 373);
            this.tpLegendaries.TabIndex = 1;
            this.tpLegendaries.Text = "Legendaries";
            this.tpLegendaries.UseVisualStyleBackColor = true;
            // 
            // lblLegendaryName
            // 
            this.lblLegendaryName.AutoSize = true;
            this.lblLegendaryName.Location = new System.Drawing.Point(6, 6);
            this.lblLegendaryName.Name = "lblLegendaryName";
            this.lblLegendaryName.Size = new System.Drawing.Size(38, 13);
            this.lblLegendaryName.TabIndex = 1;
            this.lblLegendaryName.Text = "Name:";
            // 
            // lstLegendaries
            // 
            this.lstLegendaries.FormattingEnabled = true;
            this.lstLegendaries.Location = new System.Drawing.Point(77, 6);
            this.lstLegendaries.Name = "lstLegendaries";
            this.lstLegendaries.Size = new System.Drawing.Size(268, 212);
            this.lstLegendaries.TabIndex = 0;
            // 
            // lblWeaponRarity
            // 
            this.lblWeaponRarity.AutoSize = true;
            this.lblWeaponRarity.Location = new System.Drawing.Point(42, 49);
            this.lblWeaponRarity.Name = "lblWeaponRarity";
            this.lblWeaponRarity.Size = new System.Drawing.Size(37, 13);
            this.lblWeaponRarity.TabIndex = 3;
            this.lblWeaponRarity.Text = "Rarity:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(42, 22);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(78, 13);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Weapon Type:";
            // 
            // cboWeaponRarity
            // 
            this.cboWeaponRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWeaponRarity.Enabled = false;
            this.cboWeaponRarity.FormattingEnabled = true;
            this.cboWeaponRarity.Items.AddRange(new object[] {
            "Common",
            "Uncommon",
            "Rare",
            "Legendary"});
            this.cboWeaponRarity.Location = new System.Drawing.Point(126, 46);
            this.cboWeaponRarity.Name = "cboWeaponRarity";
            this.cboWeaponRarity.Size = new System.Drawing.Size(121, 21);
            this.cboWeaponRarity.TabIndex = 1;
            this.cboWeaponRarity.SelectedIndexChanged += new System.EventHandler(this.cboRarity_SelectedIndexChanged);
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "Hammer",
            "Hand Mortar",
            "Lightning Gun",
            "Rapier",
            "Scythe",
            "Shotgun",
            "Sword",
            "Tome"});
            this.cboType.Location = new System.Drawing.Point(126, 19);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(121, 21);
            this.cboType.TabIndex = 0;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // gbCard
            // 
            this.gbCard.Controls.Add(this.btnCreateCard);
            this.gbCard.Controls.Add(this.lstCardWickedMods);
            this.gbCard.Controls.Add(this.lstCardDivineMods);
            this.gbCard.Controls.Add(this.lblCardDivineMods);
            this.gbCard.Controls.Add(this.lblCardWickedMods);
            this.gbCard.Controls.Add(this.lstCards);
            this.gbCard.Controls.Add(this.lblCards);
            this.gbCard.Controls.Add(this.lblDivineWicked);
            this.gbCard.Controls.Add(this.cboDivineWicked);
            this.gbCard.Controls.Add(this.lblCardRarity);
            this.gbCard.Controls.Add(this.cboCardRarity);
            this.gbCard.Location = new System.Drawing.Point(498, 12);
            this.gbCard.Name = "gbCard";
            this.gbCard.Size = new System.Drawing.Size(470, 542);
            this.gbCard.TabIndex = 1;
            this.gbCard.TabStop = false;
            this.gbCard.Text = "Destiny Card";
            // 
            // btnCreateCard
            // 
            this.btnCreateCard.Location = new System.Drawing.Point(31, 416);
            this.btnCreateCard.Name = "btnCreateCard";
            this.btnCreateCard.Size = new System.Drawing.Size(75, 23);
            this.btnCreateCard.TabIndex = 17;
            this.btnCreateCard.Text = "Create";
            this.btnCreateCard.UseVisualStyleBackColor = true;
            this.btnCreateCard.Click += new System.EventHandler(this.btnCreateCard_Click);
            // 
            // lstCardWickedMods
            // 
            this.lstCardWickedMods.Enabled = false;
            this.lstCardWickedMods.FormattingEnabled = true;
            this.lstCardWickedMods.Location = new System.Drawing.Point(100, 315);
            this.lstCardWickedMods.Name = "lstCardWickedMods";
            this.lstCardWickedMods.Size = new System.Drawing.Size(337, 95);
            this.lstCardWickedMods.TabIndex = 16;
            // 
            // lstCardDivineMods
            // 
            this.lstCardDivineMods.Enabled = false;
            this.lstCardDivineMods.FormattingEnabled = true;
            this.lstCardDivineMods.Location = new System.Drawing.Point(99, 212);
            this.lstCardDivineMods.Name = "lstCardDivineMods";
            this.lstCardDivineMods.Size = new System.Drawing.Size(338, 95);
            this.lstCardDivineMods.TabIndex = 15;
            // 
            // lblCardDivineMods
            // 
            this.lblCardDivineMods.AutoSize = true;
            this.lblCardDivineMods.Location = new System.Drawing.Point(28, 212);
            this.lblCardDivineMods.Name = "lblCardDivineMods";
            this.lblCardDivineMods.Size = new System.Drawing.Size(40, 13);
            this.lblCardDivineMods.TabIndex = 13;
            this.lblCardDivineMods.Text = "Divine:";
            // 
            // lblCardWickedMods
            // 
            this.lblCardWickedMods.AutoSize = true;
            this.lblCardWickedMods.Location = new System.Drawing.Point(28, 315);
            this.lblCardWickedMods.Name = "lblCardWickedMods";
            this.lblCardWickedMods.Size = new System.Drawing.Size(47, 13);
            this.lblCardWickedMods.TabIndex = 14;
            this.lblCardWickedMods.Text = "Wicked:";
            // 
            // lstCards
            // 
            this.lstCards.FormattingEnabled = true;
            this.lstCards.Location = new System.Drawing.Point(99, 49);
            this.lstCards.Name = "lstCards";
            this.lstCards.Size = new System.Drawing.Size(338, 108);
            this.lstCards.TabIndex = 12;
            // 
            // lblCards
            // 
            this.lblCards.AutoSize = true;
            this.lblCards.Location = new System.Drawing.Point(16, 49);
            this.lblCards.Name = "lblCards";
            this.lblCards.Size = new System.Drawing.Size(37, 13);
            this.lblCards.TabIndex = 11;
            this.lblCards.Text = "Cards:";
            // 
            // lblDivineWicked
            // 
            this.lblDivineWicked.AutoSize = true;
            this.lblDivineWicked.Location = new System.Drawing.Point(15, 172);
            this.lblDivineWicked.Name = "lblDivineWicked";
            this.lblDivineWicked.Size = new System.Drawing.Size(82, 13);
            this.lblDivineWicked.TabIndex = 7;
            this.lblDivineWicked.Text = "Divine/Wicked:";
            // 
            // cboDivineWicked
            // 
            this.cboDivineWicked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDivineWicked.FormattingEnabled = true;
            this.cboDivineWicked.Items.AddRange(new object[] {
            "None",
            "Divine",
            "Wicked"});
            this.cboDivineWicked.Location = new System.Drawing.Point(99, 169);
            this.cboDivineWicked.Name = "cboDivineWicked";
            this.cboDivineWicked.Size = new System.Drawing.Size(121, 21);
            this.cboDivineWicked.TabIndex = 6;
            this.cboDivineWicked.SelectedIndexChanged += new System.EventHandler(this.cboDivineWicked_SelectedIndexChanged);
            // 
            // lblCardRarity
            // 
            this.lblCardRarity.AutoSize = true;
            this.lblCardRarity.Location = new System.Drawing.Point(15, 25);
            this.lblCardRarity.Name = "lblCardRarity";
            this.lblCardRarity.Size = new System.Drawing.Size(37, 13);
            this.lblCardRarity.TabIndex = 5;
            this.lblCardRarity.Text = "Rarity:";
            // 
            // cboCardRarity
            // 
            this.cboCardRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCardRarity.FormattingEnabled = true;
            this.cboCardRarity.Items.AddRange(new object[] {
            "Common",
            "Uncommon",
            "Rare",
            "Legendary"});
            this.cboCardRarity.Location = new System.Drawing.Point(99, 22);
            this.cboCardRarity.Name = "cboCardRarity";
            this.cboCardRarity.Size = new System.Drawing.Size(121, 21);
            this.cboCardRarity.TabIndex = 4;
            this.cboCardRarity.SelectedIndexChanged += new System.EventHandler(this.cboCardRarity_SelectedIndexChanged);
            // 
            // weaponsBindingNavigator
            // 
            this.weaponsBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.weaponsBindingNavigator.BindingSource = this.weaponsBindingSource;
            this.weaponsBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.weaponsBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.weaponsBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.weaponsBindingNavigatorSaveItem});
            this.weaponsBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.weaponsBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.weaponsBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.weaponsBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.weaponsBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.weaponsBindingNavigator.Name = "weaponsBindingNavigator";
            this.weaponsBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.weaponsBindingNavigator.Size = new System.Drawing.Size(980, 25);
            this.weaponsBindingNavigator.TabIndex = 2;
            this.weaponsBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // weaponsBindingNavigatorSaveItem
            // 
            this.weaponsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.weaponsBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("weaponsBindingNavigatorSaveItem.Image")));
            this.weaponsBindingNavigatorSaveItem.Name = "weaponsBindingNavigatorSaveItem";
            this.weaponsBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.weaponsBindingNavigatorSaveItem.Text = "Save Data";
            this.weaponsBindingNavigatorSaveItem.Click += new System.EventHandler(this.weaponsBindingNavigatorSaveItem_Click);
            // 
            // weaponsBindingSource
            // 
            this.weaponsBindingSource.DataMember = "Weapons";
            this.weaponsBindingSource.DataSource = this.itemsDataSet;
            // 
            // itemsDataSet
            // 
            this.itemsDataSet.DataSetName = "ItemsDataSet";
            this.itemsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // weaponsTableAdapter
            // 
            this.weaponsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.AttacksTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CardsTableAdapter = null;
            this.tableAdapterManager.ConsumablesTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = VictorBuilder.ItemsDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.WeaponsLegendaryTableAdapter = null;
            this.tableAdapterManager.WeaponsTableAdapter = this.weaponsTableAdapter;
            // 
            // CreateItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 566);
            this.Controls.Add(this.weaponsBindingNavigator);
            this.Controls.Add(this.gbCard);
            this.Controls.Add(this.gbWeapon);
            this.Name = "CreateItemForm";
            this.Text = "CreateItem";
            this.Load += new System.EventHandler(this.CreateItemForm_Load);
            this.gbWeapon.ResumeLayout(false);
            this.gbWeapon.PerformLayout();
            this.tcWeaponMods.ResumeLayout(false);
            this.tpAffixes.ResumeLayout(false);
            this.tpAffixes.PerformLayout();
            this.tpLegendaries.ResumeLayout(false);
            this.tpLegendaries.PerformLayout();
            this.gbCard.ResumeLayout(false);
            this.gbCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponsBindingNavigator)).EndInit();
            this.weaponsBindingNavigator.ResumeLayout(false);
            this.weaponsBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbWeapon;
        private System.Windows.Forms.ComboBox cboWeaponRarity;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.GroupBox gbCard;
        private System.Windows.Forms.Label lblWeaponRarity;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TabControl tcWeaponMods;
        private System.Windows.Forms.TabPage tpAffixes;
        private System.Windows.Forms.ListBox lstSuffixes;
        private System.Windows.Forms.ListBox lstPrefixes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.TabPage tpLegendaries;
        private System.Windows.Forms.ListBox lstLegendaries;
        private System.Windows.Forms.Label lblLegendaryName;
        private System.Windows.Forms.Button btnCreateWeapon;
        private System.Windows.Forms.ListBox lstThirdAffix;
        private System.Windows.Forms.Label lblThirdAffix;
        private ItemsDataSet itemsDataSet;
        private System.Windows.Forms.BindingSource weaponsBindingSource;
        private ItemsDataSetTableAdapters.WeaponsTableAdapter weaponsTableAdapter;
        private ItemsDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator weaponsBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton weaponsBindingNavigatorSaveItem;
        private System.Windows.Forms.Label lblCardRarity;
        private System.Windows.Forms.ComboBox cboCardRarity;
        private System.Windows.Forms.Label lblDivineWicked;
        private System.Windows.Forms.ComboBox cboDivineWicked;
        private System.Windows.Forms.ListBox lstCards;
        private System.Windows.Forms.Label lblCards;
        private System.Windows.Forms.ListBox lstCardWickedMods;
        private System.Windows.Forms.ListBox lstCardDivineMods;
        private System.Windows.Forms.Label lblCardDivineMods;
        private System.Windows.Forms.Label lblCardWickedMods;
        private System.Windows.Forms.Button btnCreateCard;
    }
}