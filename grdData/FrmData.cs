using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace grdData
{
    public partial class FrmData : Form
    {
        public class CultureItm
        {
            public string CultureName { get; set; }
            public string CultureSpec { get; set; }

            public CultureItm( CultureInfo ci)
            {
                CultureName = ci.Name;
                CultureSpec = ci.DisplayName;
            }
        }

        public class GrdItem
        {
            public string   Fmt { get; set; }
            public string   Descr { get; set; }            
            public DateTime Periodo { get; set; }            

            public GrdItem(string fmt, string descr )
            {
                Fmt     = fmt;
                Descr   = descr;
                Periodo = DateTime.Parse("01/01/01");         
            }
        };

        private List<GrdItem> grdItems;

        public FrmData() { InitializeComponent(); }

        private void FrmData_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.edit_clear_list;

            //  Fill Cultures combo dropdown list
            CmbCultures_Fill();

            #region Format Sample Data
            grdItems = new List<GrdItem>(){
                new GrdItem( "d",                       "Short Date"                                  ),
                new GrdItem( "dd",                      "Day"                                         ),            
                new GrdItem( "ddd",                     "Short weekday"                               ),            
                new GrdItem( "dddd",                    "Week Day"                                    ),
                new GrdItem( "dd d",                    "Day(2 digits) + day(1/2 digits)  "           ),
                new GrdItem( "ddd d",                   "Short weekday + day"                         ),
                new GrdItem( "dddd dd",                 "Long weekday + day(2 digits)"                ),
                new GrdItem( "D",                       "Long Date"                                   ),
                                                                                                       
                new GrdItem( "m",                       "Day/Month Long Date"                         ),
                new GrdItem( "mm",                      "Minutes after hour"                          ),                                                                           
                new GrdItem( "M",                       "Day/Month Long Date"                         ),
                new GrdItem( "MM",                      "Month number"                                ),
                new GrdItem( "MMM",                     "Short Month"                                 ),
                new GrdItem( "MMMM",                    "Long Month"                                  ),
                                                                                                       
                new GrdItem( "y",                       "Month/Year Long"                             ),
                new GrdItem( "yy",                      "Year decade"                                 ),
                new GrdItem( "yyy",                     "Full year"                                   ),
                new GrdItem( "yyyy",                    "Full year"                                   ),
                new GrdItem( "Y",                       "Day/Month Long Date"                         ),
                                                                                                       
                new GrdItem( "f",                       "Full Date - Short time"                      ),            
                new GrdItem( "F",                       "Full date time"                              ),
                new GrdItem( "g",                       "General short date/time"                     ),
                new GrdItem( "G",                       "General long date/time"                      ),                                                                                                  
                                                                                                      
                new GrdItem( "ddd d / MMM",             "Short weekday + day + short month"            ),
                new GrdItem( "dd / MMM / y",            "Day + Short month + year decade"              ),
                new GrdItem( "dd / MMM / yy",           "Day + Short month + year decade"              ),
                new GrdItem( "dd / MMM / yyy",          "Day + Short month + Full year"                ),
                new GrdItem( "dd / MMMM / yyyy",        "Day + Full month + Full year"                 ),
                new GrdItem( "ddd dd / MMM / y",        "Short day + Day + Short month + year decade"  ),
                new GrdItem( "ddd dd / MMM / yy",       "Short day + Day + Short month + year decade"  ),
                new GrdItem( "ddd dd / MMM / yyy",      "Short day + Day + Short month + full year"    ),
                new GrdItem( "dddd dd / MMM / yyyy  ",  "Long weekday + day + short month + full year "),
                                                                                                        
                new GrdItem( "T",                       "Short time"                                   ),
                new GrdItem( "dd/MM/yy dddd",           "Short date + week day"                        ),
                                                                                                        
                new GrdItem( "hh",                      "Hour"                                         ),
                new GrdItem( "mm",                      "Minutes"                                      ),
                new GrdItem( "s",                       "Seconds"                                      ),
                new GrdItem( "ss",                      "Seconds 2 digits"                             ),
                new GrdItem( "h:m",                     "Hour + minutes"                               ),
                new GrdItem( "hh:mm:s tt",              "Hour + minute + seconds + AM/PM (engish)"     ),
                                                                                                       
                new GrdItem( "R",                       "Time GMT"                                     ),
                new GrdItem( "u",                       "Sort format date"                             ),
                new GrdItem( "hh:mm:s zzz",             "Hour + minute + seconds + time zone"          ),
                new GrdItem( "FF",                      "Fraction of seconds"                          ),
                new GrdItem( "hh:mm:ss.FF zzz",         "Hour + minute + seconds + seconds fraction + time zone")                
             };
            #endregion

            dgvData.DataSource = grdItems;
            //  Configutrd grid
            GrdData_Config();

            //  Seleciona a CultureInfo corrente e aplica a formatacao corresondente            
            cmbCultures.Text = CultureInfo.CurrentCulture.DisplayName;
        }

        private void CmbCultures_Fill()
        {
            foreach (CultureInfo ci in CultureInfo.GetCultures( CultureTypes.AllCultures))
            {
                cmbCultures.Items.Add( new CultureItm( ci));
            }
            cmbCultures.DisplayMember = "CultureSpec";
            cmbCultures.ValueMember   = "CultureName";
            cmbCultures.Sorted  = true;
                       
        }

        private void CmbCultures_SelectedIndexChanged(object sender, EventArgs e)
        {
            CellData_Format();  //  Monta e move formatacao para o grid
        }

        private void DtpData_ValueChanged(object sender, EventArgs e)
        {
            CellData_Format();
        }

        private void CellData_Format()
        {
            //  Monta e move formatacao para o grid
            for (int i = 0; i < grdItems.Count; i++)
            {
                //  Move a string de formatacao para montar o estilo da celula
                dgvData.Rows[i].Cells[2].Style.Format = grdItems[i].Fmt.ToString(); 
                //  Move os dados de CultureInfo para ser o bormat provider da celula
                if (cmbCultures.SelectedIndex > -1)
                {
                    dgvData.Rows[i].Cells[2].Style.FormatProvider =
                        new CultureInfo(((CultureItm)cmbCultures.Items[cmbCultures.SelectedIndex]).CultureName);
                }
                //  Move a data a ser formatada para o value da celula. Veja que o type do dado tem que ser DATETIME,
                dgvData.Rows[i].Cells[2].Value =
                    DateTime.Parse(dtpData.Text.ToString() + " " + dtpTime.Value.TimeOfDay.ToString());
            }
        }

        private void GrdData_Config()
        {
            //  A property abaixo tem que ser false caso contrario os comandos de estilo são ignorados.         
            dgvData.EnableHeadersVisualStyles = false;

            //  Header --> colors e fontes          
            dgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
            dgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //  Para conservar a mesma cor no header mesmo qdo a coluna for selecionada
            dgvData.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvData.ColumnHeadersDefaultCellStyle.BackColor;

            //  O fonte default vem do componente pai do grid, no caso o form
            //  Se vc quiser outro fonte pode escolher dentro a lista dos fontes instalados
            //       Esta é a lista ==> InstalledFontCollection font = new InstalledFontCollection();
            dgvData.ColumnHeadersDefaultCellStyle.Font = new Font(dgvData.Font.Name, dgvData.Font.Size + 1, FontStyle.Regular);
            dgvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //  Altura da linha de cabeçalho. Primeiro habilita resize e depois altera. 2.4 vezes a altura do fonte.
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvData.ColumnHeadersHeight = Convert.ToInt16(2.4 * dgvData.ColumnHeadersDefaultCellStyle.Font.Height); ;

            //  Define o estilo da linha divisoria entre os headers
            dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            //  Rows --> Cor de fundo, fonte e cor da fonte            
            dgvData.DefaultCellStyle.Font = new Font(dgvData.Font.Name, dgvData.Font.Size - 1, FontStyle.Regular);
            dgvData.DefaultCellStyle.ForeColor = Color.DarkSlateGray;

            //  Linhas alternadas de cores diferentes para facilitar a leitura
            dgvData.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            //  Altura das linhas de texto. 1.8 vezes a altura do fonte
            dgvData.RowTemplate.Height = Convert.ToInt16(1.8 * dgvData.DefaultCellStyle.Font.Height);

            //  Fazendo altura = largura por estética apenas. Nao é necessário.
            dgvData.RowHeadersWidth = dgvData.RowTemplate.Height;

            //  Columns -->
            //  O texto no header de cada coluna pode vir do datasource mas pode ser modificado aqui
            dgvData.Columns[0].HeaderText = "Format String";
            dgvData.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //  O posicionamento do texto = vertical (bottom, middle top) e horizontal(left, center, right)
            dgvData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //  Auto size em fill estende a coluna ate que ocupe todo o espaco disponivel no grid
            dgvData.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //  Coluna  2   -   Descricao
            dgvData.Columns[1].HeaderText = "Descricao";         
            dgvData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
  
            //  Coluna  3   -   Data formaada
            dgvData.Columns[2].HeaderText = "Data Formatada";            
            dgvData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //  Dimensiona a largura da coluna pelo maior conteudo entre as cels da coluna
            dgvData.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //  Bordas das celulas do grid. No caso selecionamos apenas linhas horizontais
            dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvData.GridColor = Color.BurlyWood;     // Cor das linhas

            //  Cor da parte do grid nao preenchida pela lista igual a das linhas
            dgvData.BackgroundColor = dgvData.DefaultCellStyle.BackColor;

            //  Seleção de linhas    
            //  Seleciona uma ou mais linhas ( control + click )
            dgvData.MultiSelect = false;
            //  Marca a linha toda
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //  Cor da linha selecionada
            dgvData.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            //  Limpa a selecao na apresentacao do grid
            dgvData.ClearSelection();

            //  Tira o rowheader
            dgvData.RowHeadersVisible = false;

            //  Protege o grid de modificações
            //  --------------------------------------------
            //  Protege o grid todo
            //          dgvData.ReadOnly = true;

            //  Protege apenas uma coluna
            dgvData.Columns[0].ReadOnly = true;

            //  Protege apenas uma celula.
            //          dgvData.Rows[0].Cells[0].ReadOnly = true;

            //  Impede que o usuario mude (com o mouse) a altura de linha
            dgvData.AllowUserToResizeRows = true;
            //  Permite que o usuario mude as colunas de lugar
            dgvData.AllowUserToOrderColumns = true;
        }

        private void DgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex < 0)
                {
                    MessageBox.Show("Please, right click the row you want to edit");
                } else {
                    //  Select right clicked row
                    dgvData.Rows[e.RowIndex].Selected = true;
                    //  Dims light on main form
                    this.Opacity = 0.80;

                    if (dgvData.SelectedRows.Count > 0)
                    {
                        string cellData = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
                        FrmMsg.action = "   <--";
                        using (FrmMsg frmMsg = new FrmMsg(ref cellData))
                        {
                            frmMsg.StartPosition = FormStartPosition.Manual;
                            frmMsg.Location = new Point(this.Location.X + this.Size.Width / 2,
                                                        this.Location.Y + this.Size.Height / 2); ;
                            //  Show order pad
                            frmMsg.ShowDialog();
                            if (frmMsg.DialogResult == DialogResult.OK) { };
                            if (frmMsg.DialogResult == DialogResult.Cancel) { };
                        };
                    }
                    //  Restore light on main form
                    this.Opacity = 1.0;
                }
            }
        }

        private void LnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //  Prepara a passagem de parametro para o fomr auxiliar
            CultureInfo ci = new CultureInfo(((CultureItm)cmbCultures.Items[cmbCultures.SelectedIndex]).CultureName);

            using( FrmDetails frmDetails = new FrmDetails( ci))
            { 
                //  Localizacao do from child para facilidade e visualizacao
                Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);
                frmDetails.StartPosition = FormStartPosition.Manual;                           
                frmDetails.Location = new Point( screenRectangle.Left + lnkDetails.Left, screenRectangle.Top + dgvData.Top );

                //  Show form at choosen location
                frmDetails.ShowDialog();
            }
        }
    }
}
