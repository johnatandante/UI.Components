using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Components
{

	/// <summary>
	/// Interaction logic for TextBoxPro.xaml
	/// </summary>
	public partial class TextBoxPro : UserControl, INotifyPropertyChanged
	{

		#region PropertyChanged

		public event TextChangedEventHandler TextChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		bool SetPropertyValue<T>(ref T field, T value, string propertyName) {
			if (field == null || !field.Equals(value)) {
				field = value;
				Notify(propertyName);
				return true;
			}
			return false;
		}

		void Notify(string propertyName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		public enum State
		{
			Empty,
			InUse,
		}

		State _CurrentState;
		public State CurrentState {
			get { return _CurrentState; }
			set {
				_CurrentState = value;
				if (value == State.Empty) {
					CustomFontStyle = DefaultEmptyTextFontStyle;
					SourceText = ItalicText;
					CustomForeground = DefaultEmptyTextForeground;
				} else {
					CustomFontStyle = this.FontStyle;
					CustomForeground = this.Foreground;
				}
			}
		}

		const string DefaultItalicText = "...";
		public static readonly DependencyProperty ItalicTextProperty = 
			DependencyProperty.Register("ItalicText", typeof(string), typeof(TextBoxPro),
			new FrameworkPropertyMetadata(DefaultItalicText, 
				new PropertyChangedCallback(OnItemsItalicTextPropertyChanged)));

		public string ItalicText {
			get { return (string)GetValue(ItalicTextProperty); }
			set { SetValue(ItalicTextProperty, value); }
		}

		public static Brush DefaultEmptyTextForeground = Brushes.LightGray;
		public static FontStyle DefaultEmptyTextFontStyle = FontStyles.Italic;
				
		Brush _CustomForeground;
		public Brush CustomForeground {
			get { return _CustomForeground; }
			set { SetPropertyValue<Brush>(ref _CustomForeground, value, "CustomForeground"); }
		}

		FontStyle _CustomFontStyle;
		public FontStyle CustomFontStyle {
			get { return _CustomFontStyle; }
			set { SetPropertyValue<FontStyle>(ref _CustomFontStyle, value, "CustomFontStyle"); }
		}

		string _SourceText;
		public string SourceText { 
			get { return _SourceText; }
			set { SetPropertyValue<string>(ref _SourceText, value, "SourceText"); }
		}

		public static readonly DependencyProperty TextProperty = 
			DependencyProperty.Register("Text", typeof(string), typeof(TextBoxPro),
			new PropertyMetadata(""));

		public string Text {
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public bool HasText {
			get {
				return CurrentState == State.InUse;
			}
		}

		public TextBoxPro() {
			InitializeComponent();
			
			ItalicText = DefaultItalicText;
			CurrentState = State.Empty;
			
		}

		private void SourceTextBox_TextChanged(object sender, TextChangedEventArgs e) {
			if (HasText)
				Text = SourceText;
			else
				Text = string.Empty;

			if (TextChanged != null)
				TextChanged(sender, e);

		}

		private void SourceTextBox_GotFocus(object sender, RoutedEventArgs e) {
			if (!HasText) {
				CurrentState = State.InUse;
				SourceText = string.Empty;
			}
			
		}

		private void SourceTextBox_LostFocus(object sender, RoutedEventArgs e) {
			if (HasText && string.IsNullOrEmpty(SourceText)) {				
				CurrentState = State.Empty;				
			}
		}

		private static void OnItemsItalicTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if (!(d is TextBoxPro))
				return;

			TextBoxPro tb = d as TextBoxPro;
			tb.ItalicText = e.NewValue.ToString();
			if(!tb.HasText)
				tb.CurrentState = State.Empty;

		}

	}
}
