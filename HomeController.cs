using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternProjectV2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact.";

            return View();
        }



        public ActionResult Registration()
        {
            var registrationTypes = new List<InternProjectV2.Models.RegistrationTypeListModel>
            {
            new InternProjectV2.Models.RegistrationTypeListModel { Id = 3, Name = "      " },
            new InternProjectV2.Models.RegistrationTypeListModel { Id = 1, Name = "Client" },
            new InternProjectV2.Models.RegistrationTypeListModel { Id = 2, Name = "Asignee" }
            };

            var registrationModel = new InternProjectV2.Models.RegistrationModel
            {
                RegistrationType = registrationTypes
            };

            return View(registrationModel);
        }



        public ActionResult Forgetpassword()
        {
            return View();
        }





        public ActionResult Login()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string username = model.Username;
                string password = model.Password;
                String SqlCon = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
                SqlConnection con = new SqlConnection(SqlCon);
                string SqlQuery = "select userid from dohmh.Users where username=@Username and Password=@Password";
                con.Open();
                SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    Session["UserID"] = sdr["UserID"].ToString();

                }


                con.Close();
                // check database to validate user login
                //return View("Login", model);
            }

            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Registration(Models.RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                string FirstName = model.FirstName;
                string LastName = model.LastName;
                string MiddleName = model.MiddleName;
                string DOB = model.DOB;
                string Email = model.Email;
                string username = model.Username;
                string password = model.Password;
                int RegistrationTypeID = model.RegistrationTypeID;
                string ClientName = model.ClientName;


          
                using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString))
                {
                    using (SqlCommand cmd1 = new SqlCommand("[dohmh].[USP_RegisterUser]", con1))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd1.Parameters.AddWithValue("@LastName", LastName);
                        cmd1.Parameters.AddWithValue("@MiddleName", MiddleName);
                        cmd1.Parameters.AddWithValue("@Password", password);
                        cmd1.Parameters.AddWithValue("@DOB", DOB); // Assuming you have a date of birth in the model
                        cmd1.Parameters.AddWithValue("@Email", Email);
                        cmd1.Parameters.AddWithValue("@Username", username); 
                        cmd1.Parameters.AddWithValue("@RegistrationTypeID", RegistrationTypeID); 
                        cmd1.Parameters.AddWithValue("@ClientName", ClientName); 
                        con1.Open();
                        ViewData["result"] = cmd1.ExecuteNonQuery();
                        con1.Close();
                    }
                }

            }

            return RedirectToAction("Dashboard");
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Forgetpassword(Models.ForgetPassword model)
        {
            if (ModelState.IsValid)
            {
       
                string Email = model.Email;
               

        
                using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString))
                {
                    using (SqlCommand cmd1 = new SqlCommand("[dohmh].[USP_ForgetPassword]", con1))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@userEmail", Email);
                        con1.Open();
                        ViewData["result"] = cmd1.ExecuteNonQuery();
                        con1.Close();
                    }
                }

            }

            return RedirectToAction("Dashboard");
        }
    }
}