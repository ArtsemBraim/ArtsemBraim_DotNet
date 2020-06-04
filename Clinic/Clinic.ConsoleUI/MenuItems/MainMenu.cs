using ConsoleMenu;

namespace Clinic.ConsoleUI.MenuItems
{
    public class MainMenu : IMenu
    {
        private readonly DoctorMenu _doctorMenu;
        private readonly PatientMenu _patientMenu;

        public MainMenu(DoctorMenu doctorMenu, PatientMenu patientMenu)
        {
            _doctorMenu = doctorMenu;
            _patientMenu = patientMenu;
        }

        public void Start()
        {
            while (true)
            {
                Helpers.Menu("Main menu",
                    ("Doctors", () => _doctorMenu.Start(), true),
                    ("Patients", () => _patientMenu.Start(), true));
            }
        }
    }
}
