using DocumentFormat.OpenXml.Spreadsheet;
using GestionSystemeEtudiant;
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

namespace GestionSchoolApp
{
    public partial class FormValidation: Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=GestionSystemeSchool;Integrated Security=True";
        private int userId;
        private string otpCode;
        public FormValidation(int id)
        {
            InitializeComponent();
            userId = id;
        }

        



        private void btnvalider_otp_Click(object sender, EventArgs e)
        {
            string enteredOTP = txtOTP.Text;
            string userRole = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT U.role FROM OTPCodes O " +
                               "JOIN Utilisateurs U ON O.idU = U.id " +
                               "WHERE O.idU = @id AND O.code = @code AND O.dateExp > GETDATE()";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.Parameters.AddWithValue("@code", enteredOTP);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userRole = reader["role"].ToString();
                            MessageBox.Show("Connexion réussie !");
                            this.Hide();
                            new FormAcueil(userRole).Show();  // Transmettre le rôle
                        }
                        else
                        {
                            MessageBox.Show("Code OTP invalide ou expiré !");
                        }
                    }
                }
            }
        }

        private void SendOTP()
        {
            Random rnd = new Random();
            otpCode = rnd.Next(100000, 999999).ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO OTPCodes (idU, code, dateExp) VALUES (@id, @code, @expire)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.Parameters.AddWithValue("@code", otpCode);
                    cmd.Parameters.AddWithValue("@expire", DateTime.Now.AddMinutes(5));
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show($"Votre code OTP est : {otpCode}", "Code OTP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btngenerer_Click(object sender, EventArgs e)
        {
            SendOTP();
        }
    }
    }

