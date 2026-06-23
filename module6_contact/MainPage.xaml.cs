namespace module6_contact
{
        public class Contact
        {
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string PhotoFileName { get; set; } = string.Empty;
            public string GroupKey => Name.Length > 0 ? Name[0].ToString().ToUpper() : "#";
        }

        public class ContactGroup : List<Contact>
        {
            public string GroupKey { get; }
            public ContactGroup(string key, IEnumerable<Contact> contacts) : base(contacts)
            {
                GroupKey = key;
            }
        }

        public partial class MainPage : ContentPage
        {
            public List<ContactGroup> GroupedContacts { get; set; }

            public MainPage()
            {
                InitializeComponent();

                var contacts = new List<Contact>
            {
                new Contact { Name = "Alice Morgan",  Email = "alice.morgan@email.com",  Phone = "+1 (555) 201-3344", PhotoFileName = "pic1.png", Description = "Senior software engineer passionate about distributed systems." },
                new Contact { Name = "Aaron Bell",    Email = "aaron.bell@email.com",    Phone = "+1 (555) 202-5566", PhotoFileName = "pic2.png", Description = "Product designer focused on accessibility." },
                new Contact { Name = "Amelia Stone",  Email = "amelia.stone@email.com",  Phone = "+1 (555) 203-7788", PhotoFileName = "pic3.png", Description = "Marketing specialist with 8 years of experience." },
                new Contact { Name = "Brian Carter",  Email = "brian.carter@email.com",  Phone = "+1 (555) 301-1122", PhotoFileName = "pic4.png", Description = "DevOps engineer who loves automating CI/CD pipelines." },
                new Contact { Name = "Bella Nguyen",  Email = "bella.nguyen@email.com",  Phone = "+1 (555) 302-3344", PhotoFileName = "pic1.png", Description = "Data scientist specializing in NLP and machine learning." },
                new Contact { Name = "Brandon Wells", Email = "brandon.wells@email.com", Phone = "+1 (555) 303-5566", PhotoFileName = "pic2.png", Description = "Full-stack developer with React and cloud expertise." },
                new Contact { Name = "Clara Jensen",  Email = "clara.jensen@email.com",  Phone = "+1 (555) 401-7788", PhotoFileName = "pic3.png", Description = "UX researcher translating usability studies into product wins." },
                new Contact { Name = "Carlos Rivera", Email = "carlos.rivera@email.com", Phone = "+1 (555) 402-9900", PhotoFileName = "pic4.png", Description = "Mobile developer with iOS and Android native experience." },
                new Contact { Name = "Chloe Park",    Email = "chloe.park@email.com",    Phone = "+1 (555) 403-1234", PhotoFileName = "pic1.png", Description = "Technical writer creating docs for developer platforms." },
                new Contact { Name = "Daniel Foster", Email = "daniel.foster@email.com", Phone = "+1 (555) 501-5678", PhotoFileName = "pic2.png", Description = "Cloud architect designing scalable AWS and Azure solutions." },
                new Contact { Name = "Diana Cole",    Email = "diana.cole@email.com",    Phone = "+1 (555) 502-9012", PhotoFileName = "pic3.png", Description = "Cybersecurity analyst focused on threat modeling." },
                new Contact { Name = "Dylan Marsh",   Email = "dylan.marsh@email.com",   Phone = "+1 (555) 503-3456", PhotoFileName = "pic4.png", Description = "Backend engineer specialized in high-performance APIs." },
                new Contact { Name = "Emma Hayes",    Email = "emma.hayes@email.com",    Phone = "+1 (555) 601-7890", PhotoFileName = "pic1.png", Description = "Agile project manager who ships on time." },
                new Contact { Name = "Ethan Brooks",  Email = "ethan.brooks@email.com",  Phone = "+1 (555) 602-1234", PhotoFileName = "pic2.png", Description = "Game developer experienced in Unity and Unreal Engine." },
                new Contact { Name = "Elena Torres",  Email = "elena.torres@email.com",  Phone = "+1 (555) 603-5678", PhotoFileName = "pic3.png", Description = "AI researcher exploring generative models and ethics." },
            };

                GroupedContacts = contacts
                    .GroupBy(c => c.GroupKey)
                    .OrderBy(g => g.Key)
                    .Select(g => new ContactGroup(g.Key, g.OrderBy(c => c.Name)))
                    .ToList();

                BindingContext = this;
            }

            private async void OnContactSelected(object sender, SelectionChangedEventArgs e)
            {
                if (e.CurrentSelection.FirstOrDefault() is not Contact selected)
                    return;

                ((CollectionView)sender).SelectedItem = null;
                await Navigation.PushAsync(new ContactDetailPage(selected));
            }
        }

        public class ContactDetailPage : ContentPage
        {
            public ContactDetailPage(Contact contact)
            {
                Title = "Contact Details";
                BackgroundColor = Color.FromArgb("#F2F2F7");

                var backButton = new Button
                {
                    Text = "← Back to Contacts",
                    BackgroundColor = Color.FromArgb("#007AFF"),
                    TextColor = Colors.White,
                    CornerRadius = 12,
                    Margin = new Thickness(20, 16)
                };
                backButton.Clicked += async (s, e) => await Navigation.PopAsync();

                Content = new ScrollView
                {
                    Content = new VerticalStackLayout
                    {
                        Children =
                    {
                        
                        new Image
                        {
                            Source = contact.PhotoFileName,
                            HeightRequest = 280,
                            Aspect = Aspect.AspectFill
                        },

                       
                        new VerticalStackLayout
                        {
                            BackgroundColor = Colors.White,
                            Padding = new Thickness(24, 20),
                            Margin = new Thickness(16, -30, 16, 24),
                            Spacing = 12,
                            Children =
                            {
                                new Label { Text = contact.Name,        FontSize = 24, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#1C1C1E"), HorizontalOptions = LayoutOptions.Center },
                                new BoxView { HeightRequest = 1, Color = Color.FromArgb("#E5E5EA") },
                                new Label { Text = "✉  " + contact.Email,        FontSize = 15, TextColor = Color.FromArgb("#1C1C1E") },
                                new BoxView { HeightRequest = 1, Color = Color.FromArgb("#E5E5EA") },
                                new Label { Text = "📞  " + contact.Phone,        FontSize = 15, TextColor = Color.FromArgb("#1C1C1E") },
                                new BoxView { HeightRequest = 1, Color = Color.FromArgb("#E5E5EA") },
                                new Label { Text = "💬  " + contact.Description,  FontSize = 15, TextColor = Color.FromArgb("#1C1C1E"), LineBreakMode = LineBreakMode.WordWrap },
                                backButton
                            }
                        }
                    }
                    }
                };
            }
        }
    }
