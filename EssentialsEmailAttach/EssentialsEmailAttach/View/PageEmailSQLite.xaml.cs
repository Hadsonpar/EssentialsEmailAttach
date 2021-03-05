using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EssentialsEmailAttach.Model;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace EssentialsEmailAttach.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEmailSQLite : ContentPage
    {
        //Instancia al modelo para pasar los datos del email
        EmailProperties emailProperties = new EmailProperties();
        public PageEmailSQLite()
        {
            InitializeComponent();
        }
        private async void btn_Clicked(object sender, EventArgs e)
        {
            //Otorgar permisos a la aplicación para poder acceder al archivo almacenado en el móvil - celuar
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (storageStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                storageStatus = results[Permission.Storage];
            }
            //Acceder a la ubicación de archivo para poder adjunar como parte del email a ser enviado.
            var file = Path.Combine(FileSystem.AppDataDirectory, "/storage/emulated/0/DIAbetes/RPT HADSON PERIODO MAR-2021.xlsx");

            //Acceder a la ubicación de archivo para poder adjunar como parte del email a ser enviado.
            DateTime strPeriod = DateTime.Now;
            List<string> emailTo = new List<string>();
            emailTo.Add("hadparedes@hotmail.com");

            //Asigna lo valores para cargar a los datos de nuestro modelo EmailProperties
            emailProperties.subject = "Test-01";
            emailProperties.body = "Hola!\n\nEstamos realizando envío de email haciendo uso de la API Xamarin.Essentials " +
                                    "extrayendo los valores del mensaje desde SQLite.\n\nEspero les sea útil." +
                                    "\n\nAtentamente;\nHadson!";
            emailProperties.to = emailTo;
            emailProperties.attachment = file;

            //Invoca a la tarea asincrona parando el modelo EmailProperties con el fin de realizar el envío del email
            await SendEmail(emailProperties);
        }

        public async Task SendEmail(EmailProperties pemailProperties)
        {
            try
            {
                //Propiedades del mensaje
                var message = new EmailMessage
                {
                    Subject = pemailProperties.subject,
                    Body = pemailProperties.body,
                    To = pemailProperties.to,
                };
                var fn = pemailProperties.attachment;
                var file = Path.Combine(FileSystem.CacheDirectory, fn);
                message.Attachments.Add(new EmailAttachment(file));

                //API que se encarga de abrir el cliente como el Gmail, Outlook u otros para realizar el envío del mensaje
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Email is not supported on this device 
                await DisplayAlert("Error", fnsEx.ToString(), "OK");
            }
            catch (Exception ex)
            {
                // Some other exception occurred
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
    }
}