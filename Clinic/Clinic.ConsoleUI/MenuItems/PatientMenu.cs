using Clinic.BLL.Dto;
using Clinic.BLL.Interfaces;
using ConsoleMenu;
using System;
using System.Threading.Tasks;

namespace Clinic.ConsoleUI.MenuItems
{
    public class PatientMenu : IMenu
    {
        private readonly IPatientService _patientService;

        public PatientMenu(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public void Start()
        {
            Helpers.Menu("Patient menu",
                ("Get all patients", () => GetAllPatients(), true),
                ("Delete patient", () => DeletePatient(), true),
                ("Create patient", () => CreatePatient(), true),
                ("Update patient", () => UpdatePatient(), true));
        }

        public void GetAllPatients()
        {
            var patients = _patientService.GetAll();

            foreach (var patient in patients)
            {
                ShowPatientInfo(patient);
            }

            Console.ReadKey();
        }

        public void DeletePatient()
        {
            var patients = _patientService.GetAll();
            var i = 0;
            foreach (var patient in patients)
            {
                Console.WriteLine($"Пациент №{++i}");
                ShowPatientInfo(patient);
            }

            Console.WriteLine("Выберите пациента для удаления");
            var selectedDeletedNumber = int.Parse(Console.ReadLine()) - 1;

            var deletionProcess = _patientService.DeleteAsync(patients[selectedDeletedNumber].Id);
            Task.WaitAll(deletionProcess);

            Console.WriteLine("Пациент удален успешно");

            Console.ReadKey();
        }

        public void CreatePatient()
        {
            Console.WriteLine("Введите фамилию");
            var surname = Console.ReadLine();
            Console.WriteLine("Введите имя");
            var name = Console.ReadLine();
            Console.WriteLine("Введите отчество");
            var middleName = Console.ReadLine();

            var patient = new Patient
            {
                Surname = surname,
                Name = name,
                MiddleName = middleName,
            };

            var creationProcess = _patientService.AddAsync(patient);
            Task.WaitAll(creationProcess);

            Console.WriteLine("Пациент добавлен успешно");

            Console.ReadKey();
        }

        public void UpdatePatient()
        {
            var patients = _patientService.GetAll();
            var i = 0;
            foreach (var patient in patients)
            {
                Console.WriteLine($"Пациент №{++i}");
                ShowPatientInfo(patient);
            }

            Console.WriteLine("Выберите пациента для изменения данных");
            var selectedUpdatedNumber = int.Parse(Console.ReadLine()) - 1;
            var selectedPatient = patients[selectedUpdatedNumber];
            int option;
            do
            {
                Console.WriteLine("1. Фамилия");
                Console.WriteLine("2. Имя");
                Console.WriteLine("3. Отчество");
                Console.WriteLine("0. Выход");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Введите новую фамилию:");
                        var surname = Console.ReadLine();
                        selectedPatient.Surname = surname;
                        break;
                    case 2:
                        Console.WriteLine("Введите новое имя:");
                        var name = Console.ReadLine();
                        selectedPatient.Name = name;
                        break;
                    case 3:
                        Console.WriteLine("Введите новое отчество:");
                        var middleName = Console.ReadLine();
                        selectedPatient.MiddleName = middleName;
                        break;
                    default:
                        break;
                }
                Console.Clear();
            } while (option != 0);

            Console.WriteLine("Данные пациента успешно изменены");

            Console.ReadKey();
        }

        private void ShowPatientInfo(Patient patient)
        {
            Console.WriteLine("Фамилия: " + patient.Surname);
            Console.WriteLine("Имя: " + patient.Name);
            Console.WriteLine("Отчество: " + patient.MiddleName);
            Console.WriteLine();
        }
    }
}
