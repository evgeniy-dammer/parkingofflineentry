using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Printing;
using QRCoder;
using System.Drawing;
using Npgsql;
using ParkingOfflineEntry.Properties;
using System.Configuration;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;

namespace ParkingOfflineEntry
{
    public partial class ParkingOfflineEntryMain : Form
    {
        private string currentDateTime = "";
        private string entryBarrier = (string)Settings.Default["BarrierId"];
        private SerialPort sport = new SerialPort((string)Settings.Default["SensorsComPort"], 9600, Parity.None, 8, StopBits.One);
        private bool[] underBarrier = new bool[4];
    
        private bool stage1 = false;
        private bool stage2 = false;
        private bool stage3 = false;
        private bool stage4 = false;
        private string lastval = "";

        public ParkingOfflineEntryMain() : base()
        {
            InitializeComponent();

            Show();

            FillSettings();

            FillDropdowns();

            Thread checkNeedToOpenBarrierThread = new Thread(new ThreadStart(NeedToOpenBarrier))
            {
                IsBackground = true
            };
            checkNeedToOpenBarrierThread.Start();

            Thread checkNeedToCloseBarrierThread = new Thread(new ThreadStart(NeedToCloseBarrier))
            {
                IsBackground = true
            };
            checkNeedToCloseBarrierThread.Start();

            Thread checkReadSensorsThread = new Thread(new ThreadStart(ReadSensors))
            {
                IsBackground = true
            };
            checkReadSensorsThread.Start();

            Thread checkUnderBarrierThread = new Thread(new ThreadStart(UnderBarrier))
            {
                IsBackground = true
            };
            checkUnderBarrierThread.Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            Console.WriteLine("Closed");
            sport.Close();
            base.OnClosed(e);
        }

        private NpgsqlConnection PostgreSQLConnection()
        {
            try {
                string DatabaseHost = (string)Settings.Default["DatabaseHost"];
                string DatabasePort = (string)Settings.Default["DatabasePort"];
                string DatabaseName = (string)Settings.Default["DatabaseName"];
                string DatabaseUser = (string)Settings.Default["DatabaseUser"];
                string DatabasePass = (string)Settings.Default["DatabasePass"];

                if (!String.IsNullOrEmpty(DatabaseHost) &&
                    !String.IsNullOrEmpty(DatabasePort) &&
                    !String.IsNullOrEmpty(DatabaseUser) &&
                    !String.IsNullOrEmpty(DatabasePass) &&
                    !String.IsNullOrEmpty(DatabaseName))
                {
                    var cs = "Host=" + DatabaseHost + ";" +
                        "Port=" + DatabasePort + ";" +
                        "Username=" + DatabaseUser + ";" +
                        "Password=" + DatabasePass + ";" +
                        "Database=" + DatabaseName + ";";

                    var con = new NpgsqlConnection(cs);
                    con.Open();

                    return con;
                }
                return null;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.ToString());
                throw;
            }
        }

        private void ButtonPrint_Click(System.Object sender, EventArgs e)
        {
            this.currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            PrintTicket();
            labelCarplate.Text = "";
        }

        private void PrintTicket()
        {
            if (stage1 )
            {
                Guid obj = Guid.NewGuid();
                labelCarplate.Text = obj.ToString();

                Type selectedCarType = (Type)comboBoxCarType.SelectedValue;
                Type selectedNationalityType = (Type)comboBoxNationalityType.SelectedValue;
                string ParkingId = (string)Settings.Default["ParkingId"];

                try
                {
                    bool isopen = false;
                    string tariff = "";
                    NpgsqlConnection connection = PostgreSQLConnection();

                    string query1 = "SELECT isopen FROM parking_lot WHERE id = '" + ParkingId + "'";

                    NpgsqlCommand command1 = new NpgsqlCommand(query1, connection);
                    NpgsqlDataReader reader1 = command1.ExecuteReader();

                    while (reader1.Read()) { isopen = reader1.GetBoolean(0); }

                    reader1.Close();
                    command1.Dispose();


                    string query2 = "SELECT id FROM parking_tariff WHERE nationalitytype = '" + selectedNationalityType.Value + "' AND cartype = '" + selectedCarType.Value + "' AND isopen = " + isopen + ";";

                    NpgsqlCommand command2 = new NpgsqlCommand(query2, connection);
                    NpgsqlDataReader reader2 = command2.ExecuteReader();

                    while (reader2.Read())
                    {
                        tariff = reader2.GetGuid(0).ToString();
                    }
                    reader2.Close();
                    command2.Dispose();

                    var command3 = new NpgsqlCommand { Connection = connection };

                    command3.CommandText = "INSERT INTO parking_event(id, tariff, entry, carplate, cartype, nationalitytype, parkingid, islocalevent) VALUES('" +
                        obj.ToString() + "', '" +
                        tariff + "', '" +
                        this.currentDateTime + "', '" +
                        labelCarplate.Text + "', '" +
                        selectedCarType.Value + "', '" +
                        selectedNationalityType.Value + "', '" +
                        ParkingId + "', " +
                        "true)";
                    command3.ExecuteNonQuery();
                    command3.Dispose();

                    connection.Close();
                    pictureBox1.Image = null;

                    try
                    {
                        printDocumentQRCode.PrintPage += new PrintPageEventHandler(QRDocument_PrintPage);
                        printDocumentQRCode.Print();

                        //OpenBarrier();

                        OpenBarrierWithoutPopup(obj.ToString());
                    }
                    catch (Exception msg)
                    {
                        Console.WriteLine(msg.ToString());
                        throw;
                    }
                }
                catch (Exception msg)
                {
                    Console.WriteLine(msg.ToString());
                    throw;
                }
            }
        }

        private void QRDocument_PrintPage(System.Object sender, PrintPageEventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("P/" + labelCarplate.Text + "/" + this.currentDateTime + "/C", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(4);

            pictureBox1.Image = qrCodeImage;

            pictureBox1.DrawToBitmap(qrCodeImage, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            e.Graphics.DrawImage(qrCodeImage, 0, 0);
        }

        private void FillDropdowns()
        {
            List<Type> carTypes = new List<Type>();
            List<Type> nationalityTypes = new List<Type>();

            carTypes.Add(new Type() { Value = "a8766eff-2f0e-4cb4-ab0c-5b4fd166e945", Text = "Yenil awtoulag" });
            carTypes.Add(new Type() { Value = "5a91f0dd-3675-43c3-ac41-ee6b5566d35b", Text = "Yuk, mikrawtobus 3 ton cenli" });
            carTypes.Add(new Type() { Value = "2c2b1109-c2f4-4f6d-8b79-18ec8096ef16", Text = "Yuk, awtobus 3 tonnadan agyr" });
            carTypes.Add(new Type() { Value = "d155046f-396a-47aa-91dd-fdfbd5fb5ed4", Text = "Tirkegli, yarym trk, uzyn awtobus" });
            carTypes.Add(new Type() { Value = "4494de27-639f-40a1-8de9-2df38ca2af28", Text = "Motosikl, moroller, motokolyaska" });
            carTypes.Add(new Type() { Value = "53652c41-2017-4c0b-a29c-12fbfa6912fe", Text = "Tirkegli motosikl" });
            carTypes.Add(new Type() { Value = "9a3e82ee-1adb-4082-b1f1-405fa6696aae", Text = "Moped, welosiped" });
            carTypes.Add(new Type() { Value = "4659dd31-bcbd-4f08-ac6b-6c18129aae54", Text = "Yenil awto satlyk ucin" });

            nationalityTypes.Add(new Type() { Value = "8438f408-bfa8-4408-92c7-b5292471d8c5", Text = "Turkmenistanyn rayat" });
            nationalityTypes.Add(new Type() { Value = "a1cf2b5e-e2fe-4222-adbb-85d7eb0f4b91", Text = "Dasary yurt rayat" });

            comboBoxCarType.DataSource = carTypes;
            comboBoxCarType.DisplayMember = "Text";

            comboBoxNationalityType.DataSource = nationalityTypes;
            comboBoxNationalityType.DisplayMember = "Text";
        }

        private void FillSettings()
        {
            try
            {
                textBoxParkingId.Text = (string)Settings.Default["ParkingId"];
            }
            catch (SettingsPropertyNotFoundException ex) { Console.WriteLine(ex.ToString()); }

            try
            {
                textBoxDatabaseHost.Text = (string)Settings.Default["DatabaseHost"];
            }
            catch (SettingsPropertyNotFoundException ex) { Console.WriteLine(ex.ToString()); }

            try
            {
                textBoxDatabasePort.Text = (string)Settings.Default["DatabasePort"];
            }
            catch (SettingsPropertyNotFoundException ex) { Console.WriteLine(ex.ToString()); }

            try
            {
                textBoxDatabaseName.Text = (string)Settings.Default["DatabaseName"];
            }
            catch (SettingsPropertyNotFoundException ex) { Console.WriteLine(ex.ToString()); }

            try
            {
                textBoxDatabaseUser.Text = (string)Settings.Default["DatabaseUser"];
            }
            catch (SettingsPropertyNotFoundException ex) { Console.WriteLine(ex.ToString()); }

            try
            {
                textBoxDatabasePass.Text = (string)Settings.Default["DatabasePass"];
            }
            catch (SettingsPropertyNotFoundException ex) { Console.WriteLine(ex.ToString()); }

            try
            {
                textBoxBarrierComPort.Text = (string)Settings.Default["BarrierComPort"];
            }
            catch (SettingsPropertyNotFoundException ex) { Console.WriteLine(ex.ToString()); }

            try
            {
                textBoxSensorsComPort.Text = (string)Settings.Default["SensorsComPort"];
            }
            catch (SettingsPropertyNotFoundException ex) { Console.WriteLine(ex.ToString()); }

            try
            {
                textBoxBarrierId.Text = (string)Settings.Default["BarrierId"];
            }
            catch (SettingsPropertyNotFoundException ex) { Console.WriteLine(ex.ToString()); }
        }

        private void TextBoxDatabaseHost_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["DatabaseHost"] = textBoxDatabaseHost.Text;
            Settings.Default.Save();
        }

        private void TextBoxDatabasePort_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["DatabasePort"] = textBoxDatabasePort.Text;
            Settings.Default.Save();
        }

        private void TextBoxDatabaseName_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["DatabaseName"] = textBoxDatabaseName.Text;
            Settings.Default.Save();
        }

        private void TextBoxDatabaseUser_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["DatabaseUser"] = textBoxDatabaseUser.Text;
            Settings.Default.Save();
        }

        private void TextBoxDatabasePass_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["DatabasePass"] = textBoxDatabasePass.Text;
            Settings.Default.Save();
        }

        private void TextBoxParkingId_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["ParkingId"] = textBoxParkingId.Text;
            Settings.Default.Save();
        }

        private void TextBoxBarrierComPort_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["BarrierComPort"] = textBoxBarrierComPort.Text;
            Settings.Default.Save();
        }

        private void TextBoxSensorsComPort_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["SensorsComPort"] = textBoxSensorsComPort.Text;
            Settings.Default.Save();
        }

        private void TextBoxBarrierId_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["BarrierId"] = textBoxBarrierId.Text;
            Settings.Default.Save();
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                PrintTicket();
                labelCarplate.Text = "";
            }
        }

        private void OpenBarrier()
        {
            try
            {
                if ((string)Settings.Default["BarrierComPort"] != "")
                {
                    SerialPort port = new SerialPort
                    {
                        PortName = (string)Settings.Default["BarrierComPort"],
                        BaudRate = Convert.ToInt32(9600),
                        Parity = Parity.None,
                        DataBits = 8,
                        StopBits = StopBits.One
                    };

                    if (port.IsOpen == true)
                        port.Close();

                    if (port.IsOpen == false)
                    {
                        port.Open();
                        port.ReadTimeout = 3000;
                        port.Write("1");
                        port.Close();
                    }
                }
                
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.ToString());
                throw;
            }
        }

        private void CloseBarrier()
        {
            try
            {
                if ((string)Settings.Default["BarrierComPort"] != "")
                {
                    SerialPort port = new SerialPort
                    {
                        PortName = (string)Settings.Default["BarrierComPort"],
                        BaudRate = Convert.ToInt32(9600),
                        Parity = Parity.None,
                        DataBits = 8,
                        StopBits = StopBits.One
                    };

                    if (port.IsOpen == true)
                        port.Close();

                    if (port.IsOpen == false)
                    {
                        port.Open();
                        port.ReadTimeout = 3000;
                        port.Write("2");
                        port.Close();
                    }
                } 
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.ToString());
                throw;
            }
        }

        private void ButtonCloseBarrier_Click(object sender, EventArgs e)
        {
            if (stage4 == true && stage3 == false && stage2 == false && stage1 == false)
            {
                CloseBarrier();
            }
            this.ActiveControl = ButtonPrint;
        }

        private void NeedToOpenBarrier()
        {
            while (true)
            {
                if (entryBarrier != "")
                {
                    bool needtoopen = false;

                    NpgsqlConnection connection = PostgreSQLConnection();
                    string sql = "SELECT needtoopen FROM parking_barrier WHERE id = '" + entryBarrier + "';";
                    var command = new NpgsqlCommand(sql, connection);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        needtoopen = reader.GetBoolean(0);
                    }

                    reader.Close();
                    command.Dispose();

                    if (needtoopen == true)
                    {
                        OpenBarrier();

                        var command2 = new NpgsqlCommand { Connection = connection };
                        command2.CommandText = "UPDATE parking_barrier SET needtoopen = false " +
                                            "WHERE id = '" + entryBarrier + "';";
                        command2.ExecuteNonQuery();
                        command2.Dispose();
                    }
                    connection.Close();

                    Thread.Sleep(3000);
                }
            }
        }

        private void NeedToCloseBarrier()
        {
            while (true)
            {
                if (entryBarrier != "" && stage4 == true && stage3 == false && stage2 == false && stage1 == false)
                {
                    bool needtoclose = false;

                    NpgsqlConnection connection = PostgreSQLConnection();
                    string sql = "SELECT needtoclose FROM parking_barrier WHERE id = '" + entryBarrier + "';";
                    var command = new NpgsqlCommand(sql, connection);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        needtoclose = reader.GetBoolean(0);
                    }

                    reader.Close();
                    command.Dispose();

                    if (needtoclose == true)
                    {
                        CloseBarrier();

                        var command2 = new NpgsqlCommand { Connection = connection };
                        command2.CommandText = "UPDATE parking_barrier SET needtoclose = false " +
                                            "WHERE id = '" + entryBarrier + "';";
                        command2.ExecuteNonQuery();
                        command2.Dispose();
                    }
                    connection.Close();

                    Thread.Sleep(3000);
                }
            }
        }

        private void OpenBarrierWithoutPopup(string Id)
        {
            string sum = "";
            string tariff = "";
            NpgsqlConnection connection = PostgreSQLConnection();

            string query1 = "SELECT tariff FROM parking_event WHERE id = '" + Id + "'";
            NpgsqlCommand command1 = new NpgsqlCommand(query1, connection);
            NpgsqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                tariff = reader1.GetGuid(0).ToString();
            }

            reader1.Close();
            command1.Dispose();

            string query2 = "SELECT entry FROM parking_tariff WHERE id = '" + tariff + "'";
            NpgsqlCommand command2 = new NpgsqlCommand(query2, connection);
            NpgsqlDataReader reader2 = command2.ExecuteReader();

            while (reader1.Read())
            {
                sum = reader2.GetDouble(0).ToString().Replace(',', '.');
            }

            reader2.Close();
            command2.Dispose();

            var command3 = new NpgsqlCommand { Connection = connection };
            command3.CommandText = "UPDATE parking_event SET " +
                                        "sum = " + sum + ", " +
                                        "isshown = true, " +
                                        "isaccepted = true " +
                                        "WHERE id = '" + Id + "';";
            command3.ExecuteNonQuery();
            command3.Dispose();

            var command4 = new NpgsqlCommand { Connection = connection };
            command4.CommandText = "UPDATE parking_barrier SET needtoopen = true " +
                                "WHERE id = '" + entryBarrier + "';";
            command4.ExecuteNonQuery();
            command4.Dispose();

            connection.Close();
        }

        private void UnderBarrier()
        {
            while (true)
            {
                if (underBarrier[0] == true && 
                    underBarrier[1] == true && 
                    underBarrier[2] == true && 
                    underBarrier[3] == true)
                {
                    Thread.Sleep(3000);

                    NpgsqlConnection connection = PostgreSQLConnection();

                    var command = new NpgsqlCommand { Connection = connection };
                    command.CommandText = "UPDATE parking_barrier SET needtoclose = true " +
                                        "WHERE id = '" + entryBarrier + "';";
                    command.ExecuteNonQuery();
                    command.Dispose();

                    connection.Close();

                    underBarrier[0] = false;
                    underBarrier[1] = false;
                    underBarrier[2] = false;
                    underBarrier[3] = false;
                }   
            }
        }

        private  void ReadSensors()
        {
            try
            {
                if (sport.IsOpen == true)
                    sport.Close();

                if (sport.IsOpen == false)
                {

                    sport.DataReceived += new SerialDataReceivedEventHandler(PortDatarecived);
                    sport.Open();

                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.ToString());
                throw;
            }
        }

        private void PortDatarecived(object sender, SerialDataReceivedEventArgs e)
        {
            if(lastval != Regex.Replace(sport.ReadLine().ToString(), @"\t|\n|\r", ""))
                Console.WriteLine(sport.ReadLine().ToString());
            
            lastval = Regex.Replace(sport.ReadLine().ToString(), @"\t|\n|\r", "");

            if (lastval == "000011" ||
                lastval == "000110" ||
                lastval == "001100" ||
                lastval == "000111" ||
                lastval == "001110" ||
                lastval == "001111" ||
                lastval == "001001" ||
                lastval == "001010" ||
                lastval == "000101")
            {
                stage1 = true;
                stage2 = false;
                stage3 = false;
                stage4 = false;

                if (underBarrier[0] == false &&
                    underBarrier[1] == false &&
                    underBarrier[2] == false &&
                    underBarrier[3] == false)
                {
                    underBarrier[0] = true;
                }
            }
            else if (lastval == "001100" ||
                lastval == "011000" ||
                lastval == "011100" ||
                lastval == "111000" ||
                lastval == "111100" ||
                lastval == "100100" ||
                lastval == "101000" ||
                lastval == "010100")
            {
                stage1 = false;
                stage2 = true;
                stage3 = false;
                stage4 = false;

                if (underBarrier[0] == true &&
                   underBarrier[1] == false &&
                   underBarrier[2] == false &&
                   underBarrier[3] == false)
                {
                    underBarrier[1] = true;
                }
            }
            else if (lastval == "010000" ||
                lastval == "100000" ||
                lastval == "110000")
            {
                stage1 = false;
                stage2 = false;
                stage3 = true;
                stage4 = false;

                if (underBarrier[0] == true &&
                   underBarrier[1] == true &&
                   underBarrier[2] == false &&
                   underBarrier[3] == false)
                {
                    underBarrier[2] = true;
                }
            }
            else if (lastval == "000000")
            {
                stage1 = false;
                stage2 = false;
                stage3 = false;
                stage4 = true;

                if (underBarrier[0] == true &&
                   underBarrier[1] == true &&
                   underBarrier[2] == true &&
                   underBarrier[3] == false)
                {
                    underBarrier[3] = true;
                }
                else
                {
                    underBarrier[0] = false;
                    underBarrier[1] = false;
                    underBarrier[2] = false;
                    underBarrier[3] = false;
                }
            }
        }
    }

    class Type
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
