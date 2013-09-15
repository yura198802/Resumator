using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections;
using Android.Webkit;

namespace MyFirstApplication
{
	[Activity (Label = "Резюматор", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			DataFromSummary dataFromSummary = DataFromSummary.Instance ();
			if (dataFromSummary.ResponseState) 
			{
				Dialog dialog = new Dialog (this);
				TextView text = new TextView (this);
				text.Text = dataFromSummary.Response;
				dialog.SetTitle ("Отклик на ваше резюме");
				dialog.SetContentView (text);
				dialog.Show ();
				dataFromSummary.Clear ();
			}
			dataFromSummary.Clear ();
			AddingControlFirstPage ();
		}

		private TextView _headerTextLabel;
		private TextView _labelDataName;
		private EditText _editTextDataName;
		private TextView _labelDOB;
		private DatePicker _editDOB;
		private TextView _labelGender;
		private Spinner _spinner;
		private TextView _labelWorkingPosition;
		private EditText _editWorkingPosition;
		private TextView _labelSalary;
		private EditText _editSalary;
		private TextView _labelTelephone;
		private EditText _editTelephone;
		private TextView _labelEmail;
		private EditText _editEmail;
		private Button _buttonSend;

		private void AddingControlFirstPage()
		{
			DataFromSummary dataFromSummary = DataFromSummary.Instance ();
			
			var layuot = new LinearLayout (this);
			layuot.SetBackgroundColor (Android.Graphics.Color.White);
			layuot.Orientation = Orientation.Vertical;

			_headerTextLabel = new TextView (this);
			_headerTextLabel.TextSize = 22;
			_headerTextLabel.SetTextColor (Android.Graphics.Color.Black);
			_headerTextLabel.BringToFront ();
			_headerTextLabel.Text = "Заполните Форму";
			_headerTextLabel.Gravity = GravityFlags.Center;

			_labelDataName = new TextView (this);
			_labelDataName.Text = "Введите ФИО";
			_labelDataName.SetTextColor (Android.Graphics.Color.Black);
			_editTextDataName = new EditText (this);
			_editTextDataName.InputType = Android.Text.InputTypes.ClassText|Android.Text.InputTypes.TextFlagCapWords;

			_labelDOB = new TextView (this);
			_labelDOB.Text = "Дата рождения";
			_labelDOB.SetTextColor (Android.Graphics.Color.Black);
			_editDOB = new DatePicker (this);

			_labelGender = new TextView (this);
			_labelGender.Text = "Кто вы?";
			_labelGender.SetTextColor (Android.Graphics.Color.Black);
			_spinner = new Spinner (this);

			_spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
			var adapter = ArrayAdapter.CreateFromResource (
				this, Resource.Array.gender, Android.Resource.Layout.SimpleSpinnerItem);

			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			_spinner.Adapter = adapter;

			_labelWorkingPosition = new TextView (this);
			_labelWorkingPosition.Text = "Желаемая должность";
			_labelWorkingPosition.SetTextColor (Android.Graphics.Color.Black);
			_editWorkingPosition = new EditText (this);
			_editWorkingPosition.InputType = Android.Text.InputTypes.ClassText|Android.Text.InputTypes.TextFlagCapSentences;

			_labelSalary = new TextView (this);
			_labelSalary.Text = "Ожидаемая зароботная плата";
			_labelSalary.SetTextColor (Android.Graphics.Color.Black);
			_editSalary = new EditText (this);
			_editSalary.InputType = Android.Text.InputTypes.ClassNumber;

			_labelTelephone = new TextView (this);
			_labelTelephone.Text = "Телефон";
			_labelTelephone.SetTextColor (Android.Graphics.Color.Black);
			_editTelephone = new EditText (this);
			_editTelephone.InputType = Android.Text.InputTypes.ClassPhone;

			_labelEmail = new TextView (this);
			_labelEmail.Text = "Электронный адрес";
			_labelEmail.SetTextColor (Android.Graphics.Color.Black);
			_editEmail = new EditText (this);
			_editEmail.InputType = Android.Text.InputTypes.TextVariationEmailAddress;

			_buttonSend = new Button (this);
			_buttonSend.SetBackgroundColor(Android.Graphics.Color.LightGreen);
			_buttonSend.Text = "Отправить резюме";

			_buttonSend.Click+= AddingControlsSecondPage;

			layuot.AddView (_headerTextLabel);
			layuot.AddView (_labelDataName);
			layuot.AddView (_editTextDataName);
			layuot.AddView (_labelDOB);
			layuot.AddView (_editDOB);
			layuot.AddView (_labelGender);
			layuot.AddView (_spinner);
			layuot.AddView (_labelWorkingPosition);
			layuot.AddView (_editWorkingPosition);
			layuot.AddView (_labelSalary);
			layuot.AddView (_editSalary);
			layuot.AddView (_labelTelephone);
			layuot.AddView (_editTelephone);
			layuot.AddView (_labelEmail);
			layuot.AddView (_editEmail);
			layuot.AddView (_buttonSend);
			SetContentView (layuot);

		}

		private void AddingControlsSecondPage(object sender, EventArgs e)
		{
			DataFromSummary dataFromSummary = DataFromSummary.Instance ();
			if (!Check ()) {
				return;
			}

			dataFromSummary.Name = _editTextDataName.Text;
			dataFromSummary.DOB = _editDOB.DateTime.ToString();
			dataFromSummary.Salary = _editSalary.Text;
			dataFromSummary.WorkingPosition = _editWorkingPosition.Text;
			dataFromSummary.Telephone = _editTelephone.Text;
			dataFromSummary.Email = _editEmail.Text;

			StartActivity (typeof(SecondWindow));
		}

		private bool Check()
		{
			if (_editTextDataName.Text == "" || _editSalary.Text == "" || _editWorkingPosition.Text == "" ||
				_editEmail.Text == "" || _editTelephone.Text == "") 
			{
				Dialog dialog = new Dialog (this);
				dialog.SetTitle ("Заполните все поля формы!");
				dialog.Show ();
				return false;
			}
			return true;
		}
		
		private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			DataFromSummary dataFromSummary = DataFromSummary.Instance ();
			Spinner spinner = (Spinner)sender;
			dataFromSummary.Gender = string.Format("{0}",spinner.GetItemAtPosition(e.Position));
		}
	}

	[Activity (Label = "Резюматор", MainLauncher = true)]
	public class SecondWindow : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			AddingControlsSecondPage ();
		}

		private AutoCompleteTextView _memo;
		private Button _btSender;
		private void AddingControlsSecondPage()
		{
			DataFromSummary dataFromSummary = DataFromSummary.Instance ();
		
			var tableLayout = new TableLayout (this);
			tableLayout.SetBackgroundColor (Android.Graphics.Color.White);

			AddFieldTable (tableLayout, "ФИО ", dataFromSummary.Name);
			AddFieldTable (tableLayout, "Дата рождения ", dataFromSummary.DOB);
			AddFieldTable (tableLayout, "Пол ", dataFromSummary.Gender);
			AddFieldTable (tableLayout, "Должность ", dataFromSummary.WorkingPosition);
			AddFieldTable (tableLayout, "Зарплаты ", dataFromSummary.Salary);

			var tableRowTelephone = new TableRow (this);
			tableRowTelephone.SetPadding (10, 5, 5, 10);
			tableRowTelephone.SetBackgroundColor (Android.Graphics.Color.White);
			var textViewTelephoneLabel = new TextView (this);
			textViewTelephoneLabel.SetTextColor (Android.Graphics.Color.Black);
			textViewTelephoneLabel.Text = "Телефон ";
			var textViewTelephoneData = new TextView (this);
			textViewTelephoneData.SetTextColor (Android.Graphics.Color.LightGreen);


			textViewTelephoneData.Text = (dataFromSummary.Telephone);
			textViewTelephoneData.Click+= delegate 
			{
				var uri = Android.Net.Uri.Parse ("tel:" + textViewTelephoneData.Text );
				var intent = new Intent (Intent.ActionView, uri); 
				StartActivity (intent); 
			};

			tableRowTelephone.AddView (textViewTelephoneLabel);
			tableRowTelephone.AddView (textViewTelephoneData);
			tableLayout.AddView (tableRowTelephone);

			var tableRowEmail = new TableRow (this);
			tableRowEmail.SetBackgroundColor (Android.Graphics.Color.White);
			tableRowEmail.SetPadding (10, 5, 5, 10);
			var textViewEmailLabel = new TextView (this);
			textViewEmailLabel.Text = "Электронный адрес ";
			textViewEmailLabel.SetTextColor (Android.Graphics.Color.Black);
			var textViewEmailData = new TextView (this);
			textViewEmailData.SetTextColor (Android.Graphics.Color.LightGreen);


			textViewEmailData.Text = (dataFromSummary.Email);
			textViewEmailData.Click+= delegate {
				AddHundler();
			};

			tableRowEmail.AddView (textViewEmailLabel);
			tableRowEmail.AddView (textViewEmailData);
			tableLayout.AddView (tableRowEmail);

			_memo = new AutoCompleteTextView (this);
			_memo.Text = "";
			_memo.SetHeight (200);

		    _btSender = new Button(this);
			_btSender.SetBackgroundColor(Android.Graphics.Color.LightGreen);
			_btSender.Text = "Отправить отклик";


			_memo.Visibility = Android.Views.ViewStates.Invisible;
			_btSender.Visibility = Android.Views.ViewStates.Invisible;
			tableLayout.AddView (_memo);
			tableLayout.AddView (_btSender);

			_btSender.Click += SendResponseOnFirstActivity;

			SetContentView (tableLayout);
		}

		private void AddFieldTable(TableLayout rootTableLayout, string label, string text)
		{
			var tableRow = new TableRow (this);
			tableRow.SetPadding (10, 5, 5, 10);
			tableRow.SetBackgroundColor (Android.Graphics.Color.White);
			var textViewLabel = new TextView (this);
			textViewLabel.SetTextColor (Android.Graphics.Color.Black);
			textViewLabel.Text = label;
			var textViewData = new TextView (this);
			textViewData.SetTextColor (Android.Graphics.Color.Black);
			textViewData.Text = text;
			tableRow.AddView (textViewLabel);
			tableRow.AddView (textViewData);
			rootTableLayout.AddView (tableRow);
		}

		private void AddHundler()
		{
			_memo.Visibility = Android.Views.ViewStates.Visible;
			_btSender.Visibility = Android.Views.ViewStates.Visible;
		}

		private void SendResponseOnFirstActivity(object sender, EventArgs e)
		{
			if (!(_memo.Text.Equals(""))) 
			{
				DataFromSummary dataFromSummary = DataFromSummary.Instance ();
				dataFromSummary.ResponseState = true;
				dataFromSummary.Response = _memo.Text;
				StartActivity (typeof(MainActivity));
			}
			else
			{
				Dialog dialog = new Dialog (this);
				dialog.SetTitle ("Вы пытаетесь отправить пустой отклик");
				dialog.Show ();
			}
		}
	}
}


