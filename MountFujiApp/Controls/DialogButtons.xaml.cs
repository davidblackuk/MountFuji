using Microsoft.Maui.Layouts;
using System.Windows.Input;

namespace MountFuji.Controls;

public partial class DialogButtons : ContentView
{
	#region Constatnts

	private static readonly double DefaultSize = -1.0;
	private static readonly double DefaultSpacing = 10.0;
	private static readonly string DefaultCancelText = "Cancel";
	private static readonly string DefaultOKText = "OK";

	#endregion

	#region Private Methods

	private void OnLoaded(object sender, EventArgs e)
	{
		Loaded -= OnLoaded;

		// This hack needed to work around iOS/MacCatalyst issue with buttons not having width/height values until after this event is invoked
		var timer = App.Current?.Dispatcher.CreateTimer();
		if (timer != null)
		{
			// Create a very small delay to dispatch an event handler one time
			timer.Interval = TimeSpan.FromMilliseconds(10);
			timer.IsRepeating = false;
			timer.Tick += OnTimer;
			timer.Start();
		}
	}

	private void OnTimer(object sender, EventArgs e)
	{
		// This updates the button sizes based on the properties set
		OnChange(this, 0.0, 0.0);
	}

	// Handles updating layout based on property changes
	private static void OnChange(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is DialogButtons db)
		{
			// Set margin for the Spacer based on the Direction and Spacing properties
			db.ButtonMargin = new Thickness(db.Direction == FlexDirection.Row ? db.Spacing : 0.0, db.Direction == FlexDirection.Row ? 0.0 : db.Spacing, 0.0, 0.0);

			// Set width/height requests for buttons based on CancelButtonMinimumWidthRequest, CancelButtonMinimumHeightRequest, OKButtonMinimumWidthRequest, OKButtonMinimumHeightRequest, and IsButtonSizeEqual properties
			if (db.CancelButtonMinimumWidthRequest == DialogButtons.DefaultSize && db.CancelButtonMinimumHeightRequest == DialogButtons.DefaultSize &&
				db.OKButtonMinimumWidthRequest == DialogButtons.DefaultSize && db.OKButtonMinimumHeightRequest == DialogButtons.DefaultSize &&
				db.IsButtonSizeEqual)
			{
				double width = Math.Max(db.CancelButton.Width, db.OKButton.Width);
				double height = Math.Max(db.CancelButton.Height, db.OKButton.Height);
				db.CancelButton.MinimumWidthRequest = DialogButtons.DefaultSize;
				db.CancelButton.MinimumHeightRequest = DialogButtons.DefaultSize;
				db.OKButton.MinimumWidthRequest = DialogButtons.DefaultSize;
				db.OKButton.MinimumHeightRequest = DialogButtons.DefaultSize;
				db.CancelButton.WidthRequest = width;
				db.CancelButton.HeightRequest = height;
				db.OKButton.WidthRequest = width;
				db.OKButton.HeightRequest = height;
			}
			else
			{
				db.CancelButton.WidthRequest = DialogButtons.DefaultSize;
				db.CancelButton.HeightRequest = DialogButtons.DefaultSize;
				db.OKButton.WidthRequest = DialogButtons.DefaultSize;
				db.OKButton.HeightRequest = DialogButtons.DefaultSize;
				db.CancelButton.MinimumWidthRequest = db.CancelButtonMinimumWidthRequest;
				db.CancelButton.MinimumHeightRequest = db.CancelButtonMinimumHeightRequest;
				db.OKButton.MinimumWidthRequest = db.OKButtonMinimumWidthRequest;
				db.OKButton.MinimumHeightRequest = db.OKButtonMinimumHeightRequest;
			}
		}
	}

	// Handles the Cancel button click event
	private void OnCancelButton_Clicked(object sender, EventArgs e)
	{
		CancelButtonClicked?.Invoke(this, e);
	}

	// Handles the OK button click event
	private void OnOKButton_Clicked(object sender, EventArgs e)
	{
		OKButtonClicked?.Invoke(this, e);
	}

	#endregion

	#region Bindable Properties

	// Controls the orientation of the buttons.  FlexDirection.Row for Horizontal or FlexDirection.Column for Vertical
	public static readonly BindableProperty DirectionProperty = BindableProperty.Create(nameof(Direction), typeof(FlexDirection), typeof(DialogButtons), FlexDirection.Row, BindingMode.OneWay, null, OnChange);

	// These properties are set automatically based on the platform
	public static readonly BindableProperty CancelOrderProperty = BindableProperty.Create(nameof(CancelOrder), typeof(int), typeof(DialogButtons), DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.iOS ? 1 : 3);
	public static readonly BindableProperty SpacerOrderProperty = BindableProperty.Create(nameof(SpacerOrder), typeof(int), typeof(DialogButtons), 2);
	public static readonly BindableProperty OKOrderProperty = BindableProperty.Create(nameof(OKOrder), typeof(int), typeof(DialogButtons), DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.iOS ? 3 : 1);

	// Sets the text for the Cancel button.  Default is "Cancel"
	public static readonly BindableProperty CancelTextProperty = BindableProperty.Create(nameof(CancelText), typeof(string), typeof(DialogButtons), DefaultCancelText);

	// Sets the minimum width request for the Cancel button.  Default is -1.0 (Automatic based on text)
	public static readonly BindableProperty CancelButtonMinimumWidthRequestProperty = BindableProperty.Create(nameof(CancelButtonMinimumWidthRequest), typeof(double), typeof(DialogButtons), DialogButtons.DefaultSize, BindingMode.OneWay, null, OnChange);

	// Sets the minimum height request for the Cancel button.  Default is -1.0 (Automatic based on text)
	public static readonly BindableProperty CancelButtonMinimumHeightRequestProperty = BindableProperty.Create(nameof(CancelButtonMinimumHeightRequest), typeof(double), typeof(DialogButtons), DialogButtons.DefaultSize, BindingMode.OneWay, null, OnChange);

	// Sets the style for the Cancel button
	public static readonly BindableProperty CancelButtonStyleProperty = BindableProperty.Create(nameof(CancelButtonStyle), typeof(Style), typeof(DialogButtons), null);

	// Sets the text for the OK button.  Default is "OK"
	public static readonly BindableProperty OKTextProperty = BindableProperty.Create(nameof(OKText), typeof(string), typeof(DialogButtons), DefaultOKText);

	// Sets the minimum width request for the OK button.  Default is -1.0 (Automatic based on text)
	public static readonly BindableProperty OKButtonMinimumWidthRequestProperty = BindableProperty.Create(nameof(OKButtonMinimumWidthRequest), typeof(double), typeof(DialogButtons), DialogButtons.DefaultSize, BindingMode.OneWay, null, OnChange);

	// Sets the minimum height request for the OK button.  Default is -1.0 (Automatic based on text)
	public static readonly BindableProperty OKButtonMinimumHeightRequestProperty = BindableProperty.Create(nameof(OKButtonMinimumHeightRequest), typeof(double), typeof(DialogButtons), DialogButtons.DefaultSize, BindingMode.OneWay, null, OnChange);

	// Sets the style for the OK button
	public static readonly BindableProperty OKButtonStyleProperty = BindableProperty.Create(nameof(OKButtonStyle), typeof(Style), typeof(DialogButtons), null);

	// Sets the spacing between the buttons.  Default is 10.0
	public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(DialogButtons), DialogButtons.DefaultSpacing, BindingMode.OneWay, null, OnChange);

	// This property is set automatically based on the Direction and Spacing properties
	public static readonly BindableProperty ButtonMarginProperty = BindableProperty.Create(nameof(ButtonMargin), typeof(Thickness), typeof(DialogButtons), new Thickness(DialogButtons.DefaultSpacing));

	// Determines whether button sizes are equal based on the largest button size.  Default is false.
	public static readonly BindableProperty IsButtonSizeEqualProperty = BindableProperty.Create(nameof(IsButtonSizeEqual), typeof(bool), typeof(DialogButtons), false, BindingMode.OneWay, null, OnChange);

	// Command to execute when the Cancel button is clicked
	public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(DialogButtons), null);

	// Parameter to pass to the CancelCommand when the Cancel button is clicked
	public static readonly BindableProperty CancelCommandParameterProperty = BindableProperty.Create(nameof(CancelCommandParameter), typeof(object), typeof(DialogButtons), null);

	// Command to execute when the OK button is clicked
	public static readonly BindableProperty OKCommandProperty = BindableProperty.Create(nameof(OKCommand), typeof(ICommand), typeof(DialogButtons), null);

	// Parameter to pass to the OKCommand when the OK button is clicked
	public static readonly BindableProperty OKCommandParameterProperty = BindableProperty.Create(nameof(OKCommandParameter), typeof(object), typeof(DialogButtons), null);

	#endregion

	#region Public Properties

	/// <summary>
	/// Gets or sets the orientation of the buttons.  FlexDirection.Row for Horizontal or FlexDirection.Column for Vertical
	/// </summary>
	public FlexDirection Direction
	{
		get => (FlexDirection)GetValue(DirectionProperty);
		set => SetValue(DirectionProperty, value);
	}

	/// <summary>
	/// Gets the current order of the Cancel button relative to the Spacer and OK button
	/// </summary>
	public int CancelOrder
	{
		get => (int)GetValue(CancelOrderProperty);
		private set => SetValue(CancelOrderProperty, value);
	}

	/// <summary>
	/// Gets the current order of the Spacer relative to the Cancel and OK buttons
	/// </summary>
	public int SpacerOrder
	{
		get => (int)GetValue(SpacerOrderProperty);
		private set => SetValue(SpacerOrderProperty, value);
	}

	/// <summary>
	/// Gets the current order of the OK button relative to the Cancel button and Spacer
	/// </summary>
	public int OKOrder
	{
		get => (int)GetValue(OKOrderProperty);
		private set => SetValue(OKOrderProperty, value);
	}

	/// <summary>
	/// Gets or sets the text for the Cancel button.  Default is "Cancel"
	/// </summary>
	public string CancelText
	{
		get => (string)GetValue(CancelTextProperty);
		set => SetValue(CancelTextProperty, value);
	}

	/// <summary>
	/// Gets or sets the minimum width request for the Cancel button.  Default is -1.0 (Automatic based on text)
	/// </summary>
	public double CancelButtonMinimumWidthRequest
	{
		get => (double)GetValue(CancelButtonMinimumWidthRequestProperty);
		set => SetValue(CancelButtonMinimumWidthRequestProperty, value);
	}

	/// <summary>
	/// Gets or sets the minimum height request for the Cancel button.  Default is -1.0 (Automatic based on text)
	/// </summary>
	public double CancelButtonMinimumHeightRequest
	{
		get => (double)GetValue(CancelButtonMinimumHeightRequestProperty);
		set => SetValue(CancelButtonMinimumHeightRequestProperty, value);
	}

	/// <summary>
	/// Gets or sets the style for the Cancel button
	/// </summary>
	public Style CancelButtonStyle
	{
		get => (Style)GetValue(CancelButtonStyleProperty);
		set => SetValue(CancelButtonStyleProperty, value);
	}

	/// <summary>
	/// Gets or sets the text for the OK button.  Default is "OK"
	/// </summary>
	public string OKText
	{
		get => (string)GetValue(OKTextProperty);
		set => SetValue(OKTextProperty, value);
	}

	/// <summary>
	/// Gets or sets the minimum width request for the OK button.  Default is -1.0 (Automatic based on text)
	/// </summary>
	public double OKButtonMinimumWidthRequest
	{
		get => (double)GetValue(OKButtonMinimumWidthRequestProperty);
		set => SetValue(OKButtonMinimumWidthRequestProperty, value);
	}

	/// <summary>
	/// Gets or sets the minimum height request for the OK button.  Default is -1.0 (Automatic based on text)
	/// </summary>
	public double OKButtonMinimumHeightRequest
	{
		get => (double)GetValue(OKButtonMinimumHeightRequestProperty);
		set => SetValue(OKButtonMinimumHeightRequestProperty, value);
	}

	/// <summary>
	/// Gets or sets the style for the OK button
	/// </summary>
	public Style OKButtonStyle
	{
		get => (Style)GetValue(OKButtonStyleProperty);
		set => SetValue(OKButtonStyleProperty, value);
	}

	/// <summary>
	/// Gets or sets the spacing between the buttons.  Default is 10.0
	/// </summary>
	public double Spacing
	{
		get => (double)GetValue(SpacingProperty);
		set => SetValue(SpacingProperty, value);
	}

	/// <summary>
	/// Gets current the margin for the Spacer based on the Direction and Spacing properties
	/// </summary>
	public Thickness ButtonMargin
	{
		get => (Thickness)GetValue(ButtonMarginProperty);
		private set => SetValue(ButtonMarginProperty, value);
	}

	/// <summary>
	/// Gets or sets whether button sizes are equal based on the largest button size.  Only valid if CancelButtonMinimWidthRequest, CancelButtonMinimumHeightRequest, OKButtonMinimumWidthRequest, and OKButtonMinimumHeightRequest are -1.  Default is false.
	/// </summary>
	public bool IsButtonSizeEqual
	{
		get => (bool)GetValue(IsButtonSizeEqualProperty);
		set => SetValue(IsButtonSizeEqualProperty, value);
	}

	#endregion

	#region Public Commands

	/// <summary>
	/// Command to execute when the Cancel button is clicked
	/// </summary>
	public ICommand CancelCommand
	{
		get => (ICommand)GetValue(CancelCommandProperty);
		set => SetValue(CancelCommandProperty, value);
	}

	/// <summary>
	/// Parameter to pass to the CancelCommand when the Cancel button is clicked
	/// </summary>
	public object CancelCommandParameter
	{
		get => GetValue(CancelCommandParameterProperty);
		set => SetValue(CancelCommandParameterProperty, value);
	}

	/// <summary>
	/// Command to execute when the OK button is clicked
	/// </summary>
	public ICommand OKCommand
	{
		get => (ICommand)GetValue(OKCommandProperty);
		set => SetValue(OKCommandProperty, value);
	}

	/// <summary>
	/// Parameter to pass to the OKCommand when the OK button is clicked
	/// </summary>
	public object OKCommandParameter
	{
		get => GetValue(OKCommandParameterProperty);
		set => SetValue(OKCommandParameterProperty, value);
	}

	#endregion

	#region Public Events

	public event EventHandler CancelButtonClicked;

	public event EventHandler OKButtonClicked;

	#endregion

	public DialogButtons()
	{
		Loaded += OnLoaded;

		InitializeComponent();

		CancelButton.Clicked += OnCancelButton_Clicked;
		OKButton.Clicked += OnOKButton_Clicked;
	}

	~DialogButtons()
	{
		CancelButton.Clicked -= OnCancelButton_Clicked;
		OKButton.Clicked -= OnOKButton_Clicked;
	}
}