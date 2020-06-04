using Clinic.BLL.Dto;
using Clinic.BLL.Interfaces;
using ConsoleMenu;
using System;
using System.Threading.Tasks;

namespace Clinic.ConsoleUI.MenuItems
{
    public class DoctorMenu : IMenu
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public DoctorMenu(IDoctorService doctorService, IPatientService patientService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public void Start()
        {
            Helpers.Menu("Doctor menu",
                ("Get all doctors", () => GetAllDoctors(), true),
                ("Delete doctor", () => DeleteDoctor(), true),
                ("Create doctor", () => CreateDoctor(), true),
                ("Update doctor", () => UpdateDoctor(), true),
                ("Show doctor receptions", () => ShowDoctorReceptions(), true),
                ("Add reception", () => AddReception(), true));
        }

        public void GetAllDoctors()
        {
            var doctors = _doctorService.GetAll();

            foreach (var doctor in doctors)
            {
                ShowDoctorInfo(doctor);
            }

            Console.ReadKey();
        }

        public void DeleteDoctor()
        {
            var doctors = _doctorService.GetAll();
            var i = 0;
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"Врач №{++i}");
                ShowDoctorInfo(doctor);
            }

            Console.WriteLine("Выберите врача для удаления");
            var selectedDeletedNumber = int.Parse(Console.ReadLine()) - 1;

            var deletionProcess = _doctorService.DeleteAsync(doctors[selectedDeletedNumber].Id);
            Task.WaitAll(deletionProcess);

            Console.WriteLine("Врач удален успешно");

            Console.ReadKey();
        }

        public void CreateDoctor()
        {
            Console.WriteLine("Введите фамилию");
            var surname = Console.ReadLine();
            Console.WriteLine("Введите имя");
            var name = Console.ReadLine();
            Console.WriteLine("Введите отчество");
            var middleName = Console.ReadLine();
            Console.WriteLine("Номер кабинета");
            var consultingRoom = Console.ReadLine();
            var doctor = new Doctor
            {
                Surname = surname,
                Name = name,
                MiddleName = middleName,
                ConsultingRoom = consultingRoom,
            };

            var creationProcess = _doctorService.AddAsync(doctor);
            Task.WaitAll(creationProcess);

            Console.WriteLine("Врач добавлен успешно");

            Console.ReadKey();
        }

        public void UpdateDoctor()
        {
            var doctors = _doctorService.GetAll();
            var i = 0;
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"Врач №{++i}");
                ShowDoctorInfo(doctor);
            }

            Console.WriteLine("Выберите врача для изменения данных");
            var selectedUpdatedNumber = int.Parse(Console.ReadLine()) - 1;
            var selectedDoctor = doctors[selectedUpdatedNumber];
            int option;
            do
            {
                Console.WriteLine("1. Фамилия");
                Console.WriteLine("2. Имя");
                Console.WriteLine("3. Отчество");
                Console.WriteLine("4. Номер кабинета");
                Console.WriteLine("0. Выход");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Введите новую фамилию:");
                        var surname = Console.ReadLine();
                        selectedDoctor.Surname = surname;
                        break;
                    case 2:
                        Console.WriteLine("Введите новое имя:");
                        var name = Console.ReadLine();
                        selectedDoctor.Name = name;
                        break;
                    case 3:
                        Console.WriteLine("Введите новое отчество:");
                        var middleName = Console.ReadLine();
                        selectedDoctor.MiddleName = middleName;
                        break;
                    case 4:
                        Console.WriteLine("Введите новый номер кабинета:");
                        var consultingRoom = Console.ReadLine();
                        selectedDoctor.ConsultingRoom = consultingRoom;
                        break;
                    default:
                        break;
                }
                Console.Clear();
            } while (option != 0);

            var creationProcess = _doctorService.UpdateAsync(selectedDoctor);

            Task.WaitAll(creationProcess);
            Console.WriteLine("Данные врача успешно изменены");

            Console.ReadKey();
        }

        public void ShowDoctorReceptions()
        {
            var doctors = _doctorService.GetAll();
            var i = 0;
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"Врач №{++i}");
                ShowDoctorInfo(doctor);
            }

            Console.WriteLine("Выберите врача для просмотра приёма");
            var selected = int.Parse(Console.ReadLine()) - 1;

            var gettingProcess = _doctorService.GetByIdWithPatients(doctors[selected].Id);
            Task.WaitAll(gettingProcess);

            var selectedDoctor = gettingProcess.Result;
            Console.WriteLine($"Врач: {selectedDoctor.Surname}");
            foreach (var reception in selectedDoctor.Receptions)
            {
                Console.WriteLine();
                Console.WriteLine($"Пациент: {reception.Patient.Surname}");
                Console.WriteLine($"Время приема: {reception.ReceptionTime}");
            }

            Console.ReadKey();
        }

        public void AddReception()
        {
            var doctors = _doctorService.GetAll();
            var i = 0;
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"Врач №{++i}");
                ShowDoctorInfo(doctor);
            }

            Console.WriteLine("Выберите врача для записи");
            var selectedDoctor = int.Parse(Console.ReadLine()) - 1;

            var patients = _patientService.GetAll();
            var j = 0;
            foreach (var patient in patients)
            {
                Console.WriteLine($"Пациент №{++j}");
                ShowPatientInfo(patient);
            }

            Console.WriteLine("Выберите пациента для записи");
            var selectedPatient = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Введите дату и время приёма");
            Console.WriteLine("Введите день");
            var day = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите месяц");
            var month = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите год");
            var year = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите часы");
            var hours = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите минуты");
            var minutes = int.Parse(Console.ReadLine());

            var addedProcess = _doctorService.AddReception(new Reception
            {
                DoctorId = doctors[selectedDoctor].Id,
                PatientId = patients[selectedPatient].Id,
                Patient = patients[selectedPatient],
                ReceptionTime = new DateTime(year, month, day, hours, minutes, 0),
            });

            Task.WaitAll(addedProcess);
            Console.WriteLine("Пациент успешно записан на приём");

            Console.ReadKey();
        }

        private void ShowDoctorInfo(Doctor doctor)
        {
            Console.WriteLine("Фамилия: " + doctor.Surname);
            Console.WriteLine("Имя: " + doctor.Name);
            Console.WriteLine("Отчество: " + doctor.MiddleName);
            Console.WriteLine("Номер кабинета: " + doctor.ConsultingRoom);
            Console.WriteLine();
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
