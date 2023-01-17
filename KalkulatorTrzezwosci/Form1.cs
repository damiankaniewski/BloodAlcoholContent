using System.Runtime.InteropServices;

namespace KalkulatorTrzezwosci
{
    public partial class Form1 : Form
    {
        public float wzrost;
        public float wiek;
        public float waga;
        public bool plec;
        public int spalanie;
        public int jedzenie;
        public float czasPicia;

        public int mocnyAlkohol;
        public int wino;
        public int piwo;

        public Form1()
        {
            InitializeComponent();

            txtWaga.Text = "0";
            txtWzrost.Text = "0";
            txtWiek.Text = "0";
            txtCzasPicia.Text = "0";
            numericMocnyAlkohol.Value = 0;
            numericWino.Value = 0;
            numericPiwo.Value = 0;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calculate calculate = new Calculate();

            wzrost = float.Parse(txtWzrost.Text);
            waga = float.Parse(txtWaga.Text);
            wiek = int.Parse(txtWiek.Text);
            czasPicia = float.Parse(txtCzasPicia.Text);

            if (wzrost == 0 || waga == 0 || wiek == 0 || czasPicia == 0 || (radioButtonM.Checked == false && radioButtonK.Checked == false) || (radioButtonZle.Checked == false && radioButtonNormalnie.Checked == false && radioButtonDobrze.Checked == false) || (radioButtonMalo.Checked == false && radioButtonSrednio.Checked == false && radioButtonDuzo.Checked == false))
            {
                labelWynik.Text = "B³¹d! Podaj poprawne dane!";

            }
            else
            {
                float wspolczynnik = calculate.CalculateFactor(jedzenie, spalanie, wiek, wzrost);
                float gramy = calculate.CalclateGrams(mocnyAlkohol, wino, piwo);
                float bac = calculate.CalculateBac(gramy, waga * 1000, plec);
                float final = calculate.CalculateFinal(bac, czasPicia, wspolczynnik);

                string tekst = "Wynik: " + final.ToString("0.##") + "‰";
                labelWynik.Text = tekst;
            }
        }
    
        private void numericMocnyAlkohol_ValueChanged(object sender, EventArgs e)
        {
            mocnyAlkohol = (int)numericMocnyAlkohol.Value;
        }

        private void numericWino_ValueChanged(object sender, EventArgs e)
        {
            wino = (int)numericWino.Value;
        }

        private void numericPiwo_ValueChanged(object sender, EventArgs e)
        {
            piwo = (int)numericPiwo.Value;
        }

        private void radioButtonM_CheckedChanged(object sender, EventArgs e)
        {
            plec = true;
        }

        private void radioButtonK_CheckedChanged(object sender, EventArgs e)
        {
            plec = false;
        }

        private void radioButtonZle_CheckedChanged(object sender, EventArgs e)
        {
            spalanie = 1;
        }

        private void radioButtonNormalnie_CheckedChanged(object sender, EventArgs e)
        {
            spalanie = 2;
        }

        private void radioButtonDobrze_CheckedChanged(object sender, EventArgs e)
        {
            spalanie = 3;
        }

        private void radioButtonMalo_CheckedChanged(object sender, EventArgs e)
        {
            jedzenie = 1;
        }

        private void radioButtonSrednio_CheckedChanged(object sender, EventArgs e)
        {
            jedzenie = 2;
        }

        private void radioButtonDuzo_CheckedChanged(object sender, EventArgs e)
        {
            jedzenie = 3;
        }
    
    }

    public class Calculate
    {
        public float CalculateFactor(int food, int combustion, float age, float height) // TO DO
        {
            return (food + combustion+age/20+height/15) * 0.012f;
        }
        public float CalclateGrams(int strong, int wine, int beer)
        {
            return strong * 16 + wine * 20 + beer * 20;
        }
        public float CalculateBac(float consumedGrams, float bodyWeight, bool gender)
        {
            float r;
            if (gender) //facet
            {
                r = 0.68f;
            }
            else //kobieta
            {
                r = 0.55f;
            }
            return (consumedGrams/(bodyWeight*r)) * 100;
        }
        public float CalculateFinal(float bac, float time, float factor)
        {
            float final = (bac - (time * 0.015f) - factor) * 10 + 2;
            if (final < 0)
            {
                return 0;
            }
            else
            {
                return final;
            }
            
        }
    }
}