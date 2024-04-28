using Maui.BindableProperty.Generator.Core;
using Microsoft.Maui.Layouts;
using System.Windows.Input;

namespace MountFuji.Controls;

public partial class DialogButtons : ContentView
{
	#region Constatnts

	private const double DefaultSize = -1.0;
	private const double DefaultSpacing = 10.0;
	private const string DefaultCancelText = "Cancel";
	private const string DefaultOKText = "OK";

	#endregion

	#region Private Fields/Bindable Properties

#pragma warning disable CS0169

	[AutoBindable(OnChanged = nameof(OnChange))]
	private readonly FlexDirection _direction;

	[AutoBindable(DefaultValue = "DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.iOS ? 1 : 3")]
	private readonly int _cancelOrder;

	[AutoBindable(PropertyName = "OKOrder", DefaultValue = "DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.iOS ? 3 : 1")]
	private readonly int _okOrder;

	[AutoBindable(DefaultValue = DefaultCancelText, OnChanged = nameof(OnChange))]
	private readonly string _cancelText;

	[AutoBindable(DefaultValue = nameof(DefaultSize), OnChanged = nameof(OnChange))]
	private readonly double _cancelButtonMinimumWidthRequest;

	[AutoBindable(DefaultValue = nameof(DefaultSize), OnChanged = nameof(OnChange))]
	private readonly double _cancelButtonMinimumHeightRequest;

	[AutoBindable(DefaultValue = "null")]
	private readonly Style _cancelButtonStyle;

	[AutoBindable(PropertyName = "OKText", DefaultValue = DefaultOKText, OnChanged = nameof(OnChange))]
	private readonly string _okText;

	[AutoBindable(PropertyName = "OKButtonMinimumWidthRequest", DefaultValue = nameof(DefaultSize), OnChanged = nameof(OnChange))]
	private readonly double _okButtonMinimumWidthRequest;

	[AutoBindable(PropertyName = "OKButtonMinimumHeightRequest", DefaultValue = nameof(DefaultSize), OnChanged = nameof(OnChange))]
	private readonly double _okButtonMinimumHeightRequest;

	[AutoBindable(PropertyName = "OKButtonStyle", DefaultValue = "null")]
	private readonly Style _okButtonStyle;

	[AutoBindable(DefaultValue = nameof(DefaultSpacing), OnChanged = nameof(OnChange))]
	private readonly double _spacing;

	[AutoBindable(DefaultValue = "new Thickness(DialogButtons.DefaultSpacing)")]
	private readonly Thickness _buttonMargin;

	[AutoBindable(DefaultValue = "false", OnChanged = nameof(OnChange))]
	private readonly bool _isButtonSizeEqual;

	[AutoBindable]
	private readonly ICommand _cancelCommand;

	[AutoBindable]
	private readonly object _cancelCommandParameter;

	[AutoBindable(PropertyName = "OKCommand")]
	private readonly ICommand _okCommand;

	[AutoBindable(PropertyName = "OKCommandParameter")]
	private readonly object _okCommandParameter;

#pragma warning restore CS0169

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
		OnChange(this, null, null);
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