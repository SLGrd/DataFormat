using System;
//using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;

namespace grdData
{
    public partial class FrmDetails : Form
    {
        public class GrdItem
        {
            public string GroupName  { get; set; }
            public string LocalName  { get; set; }
            public string LocalValue { get; set; }
                        
            public GrdItem(string groupName, string localName, string localValue)
            {
                GroupName  = groupName;
                LocalName  = localName;
                LocalValue = localValue;
            }
        };

        private readonly List<GrdItem> grdItems = new List<GrdItem>() {};

        //  Culture info recebido
        private readonly CultureInfo c;
        //  Variaveis de trabalho
        private string wStr, yStr, zStr;

        public FrmDetails(CultureInfo ci)
        {
            InitializeComponent();
            // Save received argument
            c = ci;
            lblCultureName.Text = c.Name + " - " + c.DisplayName 
                                + (c.DisplayName.Equals(c.NativeName) ? "" : " ==> " + c.NativeName.ToString());
        }

        private void FrmDetails_Load(object sender, EventArgs e)
        {
            //  Busca os principais itens de localizacao de data etc
            grdItems.Add(new GrdItem("DateTimeFormat", "FullDateTimePattern", c.DateTimeFormat.FullDateTimePattern));

            grdItems.Add(new GrdItem("DateTimeFormat", "LongDatePattern", c.DateTimeFormat.LongDatePattern));
            grdItems.Add(new GrdItem("DateTimeFormat", "LongTimePattern", c.DateTimeFormat.LongTimePattern));
            
            grdItems.Add(new GrdItem("DateTimeFormat", "ShortDatePattern", c.DateTimeFormat.ShortDatePattern.ToString()));
            grdItems.Add(new GrdItem("DateTimeFormat", "ShortTimePattern", c.DateTimeFormat.ShortTimePattern.ToString()));

            grdItems.Add(new GrdItem("DateTimeFormat", "AMDesignator", c.DateTimeFormat.AMDesignator));
            grdItems.Add(new GrdItem("DateTimeFormat", "PMDesignator", c.DateTimeFormat.PMDesignator));    
            
            grdItems.Add(new GrdItem("DateTimeFormat", "TimeSeparator", c.DateTimeFormat.TimeSeparator));
            grdItems.Add(new GrdItem("DateTimeFormat", "DateSeparator", c.DateTimeFormat.DateSeparator));

            wStr = ""; yStr = ""; zStr = "";
            for ( int i = 0; i < 7; i++) {
                wStr = wStr + c.DateTimeFormat.ShortestDayNames[i].ToString(); 
                yStr = yStr + c.DateTimeFormat.AbbreviatedDayNames[i].ToString();
                zStr = zStr + c.DateTimeFormat.DayNames[i].ToString();
                if ( i < 6) { wStr += ", "; yStr += ", "; zStr += ", "; }
            }
            grdItems.Add(new GrdItem("DateTimeFormat", "ShortestDayNames", wStr));
            grdItems.Add(new GrdItem("DateTimeFormat", "AbbreviatedDayNames", yStr));
            grdItems.Add(new GrdItem("DateTimeFormat", "DayNames", zStr));

            wStr = ""; yStr = "";
            for (int i = 0; i < 12; i++) {
                wStr = wStr + c.DateTimeFormat.AbbreviatedMonthGenitiveNames[i].ToString();
                yStr = yStr + c.DateTimeFormat.AbbreviatedMonthNames[i].ToString();
                if (i < 11) { wStr += ", "; yStr += ", "; }
            }
            grdItems.Add(new GrdItem("DateTimeFormat", "AbbreviatedMonthGenitiveNames", wStr));
            grdItems.Add(new GrdItem("DateTimeFormat", "AbbreviatedMonthNames", yStr));

            grdItems.Add(new GrdItem("DateTimeFormat", "CalendarWeekRule", c.DateTimeFormat.CalendarWeekRule.ToString()));
            grdItems.Add(new GrdItem("DateTimeFormat", "FirstDayOfWeek", c.DateTimeFormat.FirstDayOfWeek.ToString()));

            grdItems.Add(new GrdItem("DateTimeFormat", "SortableDateTimePattern", c.DateTimeFormat.SortableDateTimePattern.ToString()));
            grdItems.Add(new GrdItem("DateTimeFormat", "UniversalSortableDateTimePattern", c.DateTimeFormat.UniversalSortableDateTimePattern));
            grdItems.Add(new GrdItem("DateTimeFormat", "MonthDayPattern", c.DateTimeFormat.MonthDayPattern));
            grdItems.Add(new GrdItem("DateTimeFormat", "YearMonthPattern", c.DateTimeFormat.YearMonthPattern));

            grdItems.Add(new GrdItem("DateTimeFormat", "NativeCalendarName", c.DateTimeFormat.NativeCalendarName));          

            grdItems.Add(new GrdItem("NumberFormat", "NumberDecimalSeparator", c.NumberFormat.NumberDecimalSeparator));
            //  Move os dados para p grid
            dgvCultureDetails.DataSource = grdItems;
            //  Configura a apresentacao do grid
            DgvCultureInfo_Config();
        }

        private void DgvCultureInfo_Config()
        {
            //  A property abaixo tem que ser false caso contrario os comandos de estilo são ignorados.         
            dgvCultureDetails.EnableHeadersVisualStyles = false;
            dgvCultureDetails.AutoGenerateColumns = false;
            
            //  Header --> colors e fontes          
            dgvCultureDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
            dgvCultureDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //  Para conservar a mesma cor no header mesmo qdo a coluna for selecionada
            dgvCultureDetails.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvCultureDetails.ColumnHeadersDefaultCellStyle.BackColor;

            //  O fonte default vem do componente pai do grid, no caso o form
            dgvCultureDetails.ColumnHeadersDefaultCellStyle.Font = new Font(dgvCultureDetails.Font.Name, dgvCultureDetails.Font.Size + 4, FontStyle.Regular);
            dgvCultureDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //  Altura da linha de cabeçalho. Primeiro habilita resize e depois altera. 2.4 vezes a altura do fonte.
            dgvCultureDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvCultureDetails.ColumnHeadersHeight = Convert.ToInt16(2.4 * dgvCultureDetails.ColumnHeadersDefaultCellStyle.Font.Height); ;

            //  Define o estilo da linha divisoria entre os headers
            dgvCultureDetails.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            //  Rows --> Cor de fundo, fonte e cor da fonte            
            dgvCultureDetails.DefaultCellStyle.Font = new Font(dgvCultureDetails.Font.Name, dgvCultureDetails.Font.Size + 2, FontStyle.Regular);
            dgvCultureDetails.DefaultCellStyle.ForeColor = Color.DarkSlateGray;

            //  Linhas alternadas de cores diferentes para facilitar a leitura
            dgvCultureDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            //  Altura das linhas de texto. 1.8 vezes a altura do fonte
            //dgvCultureDetails.RowHeadersHeightSizeMode = DataGridViewRowHeadersHeightSizeMode.EnableResizing;
            dgvCultureDetails.RowTemplate.Height = Convert.ToInt16( 2 * dgvCultureDetails.DefaultCellStyle.Font.Height);

            //  Fazendo altura = largura por estética apenas. Nao é necessário.
            //dgvCultureDetails.RowHeadersWidth = dgvCultureDetails.RowTemplate.Height;

            //  Columns -->
            //  O texto no header de cada coluna pode vir do datasource mas pode ser modificado aqui
            dgvCultureDetails.Columns[0].HeaderText = "Grupo";
            dgvCultureDetails.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //  O posicionamento do texto = vertical (bottom, middle top) e horizontal(left, center, right)
            dgvCultureDetails.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //  Auto size em fill estende a coluna ate que ocupe todo o espaco disponivel no grid
            dgvCultureDetails.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //  Coluna  2   -   Descricao
            dgvCultureDetails.Columns[1].HeaderText = "Item";
            dgvCultureDetails.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCultureDetails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //  Coluna  3   -   Data formaada
            dgvCultureDetails.Columns[2].HeaderText = "Regionalização";
            dgvCultureDetails.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //  Dimensiona a largura da coluna pelo maior conteudo entre as cels da coluna
            dgvCultureDetails.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //  Bordas das celulas do grid. No caso selecionamos apenas linhas horizontais
            dgvCultureDetails.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCultureDetails.GridColor = Color.BurlyWood;     // Cor das linhas

            //  Cor da parte do grid nao preenchida pela lista igual a das linhas
            dgvCultureDetails.BackgroundColor = dgvCultureDetails.DefaultCellStyle.BackColor;

            //  Seleção de linhas    
            //  Seleciona uma ou mais linhas ( control + click )
            dgvCultureDetails.MultiSelect = false;
            //  Marca a linha toda
            dgvCultureDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //  Cor da linha selecionada
            dgvCultureDetails.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            //  Limpa a selecao na apresentacao do grid
            dgvCultureDetails.ClearSelection();

            //  Tira o rowheader
            dgvCultureDetails.RowHeadersVisible = false;

            //  Protege o grid de modificações
            //  --------------------------------------------
            //  Protege o grid todo
            //          dgvCultureDetails.ReadOnly = true;

            //  Protege apenas uma coluna
            dgvCultureDetails.Columns[0].ReadOnly = true;

            //  Protege apenas uma celula.
            //          dgvCultureDetails.Rows[0].Cells[0].ReadOnly = true;

            //  Impede que o usuario mude (com o mouse) a altura de linha
            dgvCultureDetails.AllowUserToResizeRows = true;
            //  Permite que o usuario mude as colunas de lugar
            dgvCultureDetails.AllowUserToOrderColumns = true;
        }
    }
}
