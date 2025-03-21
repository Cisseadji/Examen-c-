using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio;
using System.Net;
using System.Net.Mail;
using Twilio.Rest.Api.V2010.Account;
using BCrypt.Net;

namespace GestionSchoolApp.Forms
{
    public partial class FormLogin : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=GestionSystemeSchool;Integrated Security=True";
        private int userId;
        private string otpCode;

        public FormLogin()
        {
            InitializeComponent();
            panelOTP.Visible = false;

        }


        

            
            
            
        

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Text;
            string userRole = "";  // Variable pour stocker le rôle

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, mdp, role FROM Utilisateurs WHERE nomUser = @username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPassword = reader["mdp"].ToString();
                            if (BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                            {
                                userId = Convert.ToInt32(reader["id"]);
                                userRole = reader["role"].ToString();  // Récupérer le role
                                this.Hide();
                                new FormValidation(userId).Show();
                            }
                            else
                            {
                                MessageBox.Show("Mot de passe incorrect !");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Utilisateur introuvable !");
                        }
                    }
                }
            }
        }




       


       

        private void FormLogin_Load(object sender, EventArgs e)
        {
           
        }
    }
}
    

