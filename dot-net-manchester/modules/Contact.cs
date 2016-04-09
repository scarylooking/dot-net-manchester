using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Security;
using System.Threading.Tasks;
using wpug.models;
using wpug.utility;

namespace wpug.modules
{
    public class Contact : NancyModule
    {
        private readonly string pagename = "Contact Us";

        public Contact()
        {
            Post["/contact"] = parameters => {
                try
                {
                    this.ValidateCsrfToken();
                }
                catch (CsrfValidationException)
                {
                    return HttpStatusCode.Forbidden;
                }

                var reCaptchaToken = Request.Form["grecaptcharesponse"].Value;

                var model = this.BindTo(modelBase());

                if (validateForm(ref model, reCaptchaToken))
                {
                    // Process the form
                    cleanModelOnSuccess(ref model);
                }

                return View["contact", model];
            };

            Get["/contact"] = _ => navigateToContactView();
            Get["/contactus"] = _ => navigateToContactView();
            Get["/contact-us"] = _ => navigateToContactView();
        }

        private ContactFormPage modelBase()
        {
            return new ContactFormPage() { Title = pagename };
        }

        private Negotiator navigateToContactView()
        {
            return View["contact", modelBase()];
        }

        private void cleanModelOnSuccess(ref ContactFormPage model)
        {
            model.submitted = true;
            model.name = string.Empty;
            model.emailAddress = string.Empty;
            model.message = string.Empty;
            model.status = new ContactFormStatus();
        }

        private bool validateForm (ref ContactFormPage model, string reCaptchaToken)
        {
            var submissionValid = true;

            if (!ReCaptcha.Validate(reCaptchaToken))
            {
                model.status.recaptcha = false;
                submissionValid = false;
            }

            if (string.IsNullOrWhiteSpace(model.emailAddress))
            {
                model.status.email = false;
                submissionValid = false;
            }

            if (string.IsNullOrWhiteSpace(model.message))
            {
                model.status.message = false;
                submissionValid = false;
            }

            if (string.IsNullOrWhiteSpace(model.name))
            {
                model.status.name = false;
                submissionValid = false;
            }

            return submissionValid;
        }
    }
}