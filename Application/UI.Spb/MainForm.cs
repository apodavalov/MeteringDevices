using MeteringDevices.Data;
using MeteringDevices.Data.Spb;
using ModernRoute.WildData.Core;
using Ninject;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MeteringDevices.UI.Spb
{
    public partial class MainForm : Form
    {
        private const double _WeekLength = 7.0;
        private ISessionFactory _SessionFactory;

        public MainForm()
        {
            InitializeComponent();
        }

        private void bt_Send_Click(object sender, EventArgs e)
        {
            try
            {
                using (ISession session = _SessionFactory.OpenSession())
                {
                    using (session.Transaction = session.BeginTransaction(IsolationLevel.Snapshot))
                    {
                        if (!CheckEnable(session, false))
                        {
                            Close();
                        }

                        int day = GetTextBoxIntValue(tb_Day);
                        int night = GetTextBoxIntValue(tb_Night);
                        int kitchenCold = GetTextBoxIntValue(tb_KitchenCold);
                        int kitchenHot = GetTextBoxIntValue(tb_KitchenHot);
                        int bathroomCold = GetTextBoxIntValue(tb_BathroomCold);
                        int bathroomHot = GetTextBoxIntValue(tb_BathroomHot);

                        CurrentMeteringValue currentValue = session.SpbCurrentMeteringValueRepository.Fetch().AsEnumerable().SingleOrDefault();

                        DialogResult dialogResult;

                        if (currentValue != null)
                        {
                            int diffDay = Math.Abs(currentValue.Day - day);
                            int diffNight = Math.Abs(currentValue.Night - night);
                            int diffKitchenCold = Math.Abs(currentValue.KitchenCold - kitchenCold);
                            int diffKitchenHot = Math.Abs(currentValue.KitchenHot - kitchenHot);
                            int diffBathroomCold = Math.Abs(currentValue.BathroomCold - bathroomCold);
                            int diffBathroomHot = Math.Abs(currentValue.BathroomHot - bathroomHot);

                            dialogResult = MessageBox.Show(
                                string.Format("Разница между предсказанными и введёнными значениями.{0}{0}" + 
                                              "Дневное энергопотребление.{0}- Предсказанное: {1}, введённое: {2}, разница (абс. зн.): {3}.{0}" +
                                              "Ночное энергопотребление.{0}- Предсказанное: {4}, введённое: {5}, разница (абс. зн.): {6}.{0}" +
                                              "Холодная вода (кухня).{0}- Предсказанное: {7}, введённое: {8}, разница (абс. зн.): {9}.{0}" +
                                              "Горячая вода (кухня).{0}- Предсказанное: {10}, введённое: {11}, разница (абс. зн.): {12}.{0}" +
                                              "Холодная вода (ванная).{0}- Предсказанное: {13}, введённое: {14}, разница (абс. зн.): {15}.{0}" +
                                              "Горячая вода (ванная).{0}- Предсказанное: {16}, введённое: {17}, разница (абс. зн.): {18}.{0}" +
                                              "{0}Подтверждаете отправку введённых значений?", 
                                              Environment.NewLine,
                                              currentValue.Day,
                                              day,
                                              diffDay,
                                              currentValue.Night,
                                              night, 
                                              diffNight,
                                              currentValue.KitchenCold,
                                              kitchenCold,
                                              diffKitchenCold,
                                              currentValue.KitchenHot,
                                              kitchenHot,
                                              diffKitchenHot,
                                              currentValue.BathroomCold,
                                              bathroomCold,
                                              diffBathroomCold,
                                              currentValue.BathroomHot,
                                              bathroomHot,
                                              diffBathroomHot), 
                                              "Подтверждение", 
                                              MessageBoxButtons.YesNo, 
                                              MessageBoxIcon.Question, 
                                              MessageBoxDefaultButton.Button2);
                        }
                        else
                        {
                            dialogResult = DialogResult.Yes;
                        }

                        if (dialogResult != DialogResult.Yes)
                        {
                            return;
                        }

                        MeteringValue value = new MeteringValue
                        {
                            Day = day,
                            Night = night,
                            KitchenCold = kitchenCold,
                            KitchenHot = kitchenHot,
                            BathroomCold = bathroomCold,
                            BathroomHot = bathroomHot
                        };

                        WriteResult result = session.SpbMeteringValueRepository.Store(value);

                        if (result.ResultType == WriteResultType.Ok)
                        {
                            MessageBox.Show("Показания приборов учёта успешно переданы. Спасибо!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            throw new InvalidOperationException(string.Format("Ошибка сохранения, код результата {0}, сообщение: {1}.",result.ResultType, result.Message));
                        }                        

                        session.Transaction.Commit();
                        session.Transaction = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private int GetTextBoxIntValue(TextBox textBox)
        {
            int value;

            if (!int.TryParse(textBox.Text, out value) || value < 0)
            {
                textBox.ForeColor = Color.Red;

                throw new InvalidOperationException("Одно или несколько из введённых значений некорректно(ы). Проверьте правильность ввёденных данных. Все значения должны быть целыми неотрицательными числами.");
            }

            return value;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _SessionFactory = Program.Kernel.Get<ISessionFactory>();

            try
            {
                using (ISession session = _SessionFactory.OpenSession())
                {
                    if (!CheckEnable(session, true))
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
                Close();
            }
        }

        private bool CheckEnable(ISession session, bool formLoading)
        {
            MeteringValue value = session.SpbMeteringValueRepository.Fetch().OrderByDescending(v => v.When).Take(1).AsEnumerable().SingleOrDefault();

            if (value != null)
            {
                if ((DateTime.UtcNow - value.When).TotalDays < _WeekLength)
                {
                    MessageBox.Show("Показания приборов учёта вводились недавно. В данный момент ввод новых показаний не требуется.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (formLoading)
            {
                CurrentMeteringValue currentValue = session.SpbCurrentMeteringValueRepository.Fetch().AsEnumerable().SingleOrDefault();

                if (currentValue != null)
                {
                    tb_Night.Text = currentValue.Night.ToString();
                    tb_Day.Text = currentValue.Day.ToString();
                    tb_KitchenCold.Text = currentValue.KitchenCold.ToString();
                    tb_KitchenHot.Text = currentValue.KitchenHot.ToString();
                    tb_BathroomCold.Text = currentValue.BathroomCold.ToString();
                    tb_BathroomHot.Text = currentValue.BathroomHot.ToString();
                }
            }

            return true;
        }

        private static void ShowError(Exception exception)
        {
            MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                textBox.ForeColor = SystemColors.WindowText;
            }
        }
    }
}
