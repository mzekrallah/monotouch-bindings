//
// coreplot.cs: API binding to the CorePlot library
//
// TODO: events for CPAxis
//
// Author:
//   Miguel de Icaza
//
using MonoMac;
using MonoMac.Foundation;
using MonoMac.CoreGraphics;
using MonoMac.ObjCRuntime;
using System;
using System.Drawing;
using MonoMac.CoreAnimation;
#if MONOTOUCH
using MonoTouch.UIKit;
#else
using MonoMac.AppKit;
#endif

namespace MonoMac.CorePlot {

	[BaseType (typeof (NSObject))]
	interface CPAnnotation {
		[Export ("contentLayer")]
		CPLayer ContentLayer { get; set; }

		[Export ("annotationHostLayer")]
		CPAnnotationHostLayer AnnotationHostLayer { get; set; }

		[Export ("contentAnchorPoint")]
		PointF ContenAnchorPoint { get; set; }

		[Export ("displacement")]
		PointF Displacement { get; set; }

		[Export ("rotation")]
		float Rotation { get; set; }

		[Export ("positionContentLayer")]
		void PositionContentLayer ();
	}

	[BaseType (typeof (CPLayer))]
	interface CPAnnotationHostLayer {
		[Export ("annotations")]
		CPAnnotation [] Annotations { get; }

		[Export ("addAnnotation:")]
		void Add (CPAnnotation annotation);

		[Export ("removeAnnotation:")]
		void Remove (CPAnnotation annotation);

		[Export ("removeAllAnnotations")]
		void RemoveAll ();
	}


	[BaseType (typeof (NSObject))]
	[Model]
	interface CPAxisDelegate {
		[Abstract, DelegateName ("CPAxisPredicate"), DefaultValue (false)]
		[Export ("axisShouldRelabel:")]
		bool AxisShouldRelabel (CPAxis axis);

		[Abstract, DelegateName ("CPAxisPredicate")]
		[Export ("axisDidRelabel:")]
		void AxisDidRelabel (CPAxis axis);

		[Abstract, DelegateName ("CPAxisNSSetPredicate"), DefaultValue (false)]
		[Export ("axis:shouldUpdateAxisLabelsAtLocations:")]
		bool ShouldUpdateAxisLablesAtLocations (CPAxis axis, NSSet locations);
	}

	[BaseType (typeof (CPLayer), Delegates=new string [] {"WeakDelegate"}, Events=new Type [] { typeof (CPAxisDelegate)})]
	interface CPAxis {
		[Export ("delegates"), NullAllowed, New]
		NSObject WeakDelegate { get; set;  }

		[Wrap ("WeakDelegate"), New]
		CPAxisDelegate Delegate { get; set; }

		[Export ("axisLineStyle")]
		CPLineStyle AxisLineStyle { get; set;  }

		[Export ("coordinate")]
		CPCoordinate Coordinate { get; set;  }

#if DECIMAL
		[Export ("labelingOrigin")]
		NSDecimal LabelingOrigin { get; set;  }

		[Export ("titleLocation")]
		NSDecimal TitleLocation { get; set;  }

		[Export ("defaultTitleLocation")]
		NSDecimal DefaultTitleLocation { get;  }

		[Export ("majorIntervalLength")]
		NSDecimal MajorIntervalLength { get; set;  }
#endif

		[Export ("tickDirection")]
		CPSign TickDirection { get; set;  }

		[Export ("visibleRange")]
		CPPlotRange VisibleRange { get; set;  }

		[Export ("titleTextStyle")]
		CPTextStyle TitleTextStyle { get; set;  }

		[Export ("axisTitle")]
		CPAxisTitle AxisTitle { get; set;  }

		[Export ("titleOffset")]
		float TitleOffset { get; set;  }

		[Export ("title")]
		string Title { get; set;  }

		[Export ("labelingPolicy")]
		CPAxisLabelingPolicy LabelingPolicy { get; set;  }

		[Export ("labelOffset")]
		float LabelOffset { get; set;  }

		[Export ("labelRotation")]
		float LabelRotation { get; set;  }

		[Export ("labelAlignment")]
		CPAlignment LabelAlignment { get; set;  }

		[Export ("labelTextStyle")]
		CPTextStyle LabelTextStyle { get; set;  }

		[Export ("labelFormatter")]
		/*NSNumberFormatter*/ NSObject LabelFormatter { get; set;  }

		[Export ("axisLabels")]
		NSSet AxisLabels { get; set;  }

		[Export ("needsRelabel")]
		bool NeedsRelabel { get;  }

		[Export ("labelExclusionRanges")]
		NSObject [] LabelExclusionRanges { get; set;  }

		[Export ("majorTickLength")]
		float MajorTickLength { get; set;  }

		[Export ("majorTickLineStyle")]
		CPLineStyle MajorTickLineStyle { get; set;  }

		[Export ("majorTickLocations")]
		NSSet MajorTickLocations { get; set;  }

		[Export ("preferredNumberOfMajorTicks")]
		int PreferredNumberOfMajorTicks { get; set;  }

		[Export ("minorTicksPerInterval")]
		int MinorTicksPerInterval { get; set;  }

		[Export ("minorTickLength")]
		float MinorTickLength { get; set;  }

		[Export ("minorTickLineStyle")]
		CPLineStyle MinorTickLineStyle { get; set;  }

		[Export ("minorTickLocations")]
		NSSet MinorTickLocations { get; set;  }

		[Export ("majorGridLineStyle")]
		CPLineStyle MajorGridLineStyle { get; set;  }

		[Export ("minorGridLineStyle")]
		CPLineStyle MinorGridLineStyle { get; set;  }

		[Export ("gridLinesRange")]
		CPPlotRange GridLinesRange { get; set;  }

		[Export ("alternatingBandFills")]
		NSObject AlternatingBandFills { get; set;  }

		[Export ("backgroundLimitBands")]
		/* NSMutableArray */ NSArray BackgroundLimitBands { get;  }

		[Export ("plotSpace")]
		CPPlotSpace PlotSpace { get; set;  }

		[Export ("separateLayers")]
		bool SeparateLayers { get; set;  }

		[Export ("plotArea")]
		CPPlotArea PlotArea { get; set;  }

		[Export ("minorGridLines")]
		CPGridLines MinorGridLines { get;  }

		[Export ("majorGridLines")]
		CPGridLines MajorGridLines { get;  }

		[Export ("axisSet")]
		CPAxisSet AxisSet { get;  }

		[Export ("relabel")]
		void Relabel ();

		[Export ("setNeedsRelabel")]
		void SetNeedsRelabel ();

		[Export ("filteredMajorTickLocations:")]
		NSSet FilteredMajorTickLocations (NSSet allLocations);

		[Export ("filteredMinorTickLocations:")]
		NSSet FilteredMinorTickLocations (NSSet allLocations);

		[Export ("addBackgroundLimitBand:")]
		void AddBackgroundLimitBand (CPLimitBand limitBand);

		[Export ("removeBackgroundLimitBand:")]
		void RemoveBackgroundLimitBand (CPLimitBand limitBand);

#if DECIMAL
		[Abstract, Export ("viewPointForCoordinateDecimalNumber:")]
		PointF ViewPointForCoordinateDecimalNumber (NSDecimal coordinateDecimalNumber);
#endif
		[Export ("drawGridLinesInContext:isMajor:")]
		void DrawGridLines (CGContext context, bool major);

		[Export ("drawBackgroundBandsInContext:")]
		void DrawBackgroundBands (CGContext context);

		[Export ("drawBackgroundLimitsInContext:")]
		void DrawBackgroundLimits (CGContext context);
	}

	[BaseType (typeof (NSObject))]
	interface CPAxisLabel {
		[Export ("contentLayer")]
		CPLayer ContentLayer { get; set;  }

		[Export ("offset")]
		float Offset { get; set;  }

		[Export ("rotation")]
		float Rotation { get; set;  }

		[Export ("alignment")]
		CPAlignment Alignment { get; set;  }

#if DECIMAL
		[Export ("tickLocation")]
		NSDecimal TickLocation { get; set;  }
#endif

		[Export ("initWithText:textStyle:")]
		IntPtr Constructor (string newText, CPTextStyle style);

		[Export ("initWithContentLayer:")]
		IntPtr Constructor (CPLayer layer);

		[Export ("positionRelativeToViewPoint:forCoordinate:inDirection:")]
		void ComputePositionRelative (PointF viewPoint, CPCoordinate forCoordinate, CPSign inDirection);

		[Export ("positionBetweenViewPoint:andViewPoint:forCoordinate:inDirection:")]
		void ComputePositionBetween (PointF firstPoint, PointF secondPoint, CPCoordinate coordinate, CPSign direction);
	}

	[BaseType (typeof (CPLayer))]
	interface CPAxisLabelGroup {
	}

	[BaseType (typeof (CPAxisLabel))]
	interface CPAxisTitle {
	}

	[BaseType (typeof (CPLayer))]
	interface CPAxisSet {
		[Export ("axes")]
		CPAxis [] Axes { get; set;  }

		[Export ("borderLineStyle")]
		CPLineStyle BorderLineStyle { get; set;  }

		[Export ("relabelAxes")]
		void RelabelAxes ();
	}

	[BaseType (typeof (CPPlot))]
	interface CPBarPlot {
		[Export ("barWidth")]
		float BarWidth { get; set;  }

		[Export ("barOffset")]
		float BarOffset { get; set;  }

		[Export ("lineStyle")]
		CPLineStyle LineStyle { get; set;  }

		[Export ("fill")]
		CPFill Fill { get; set;  }

		[Export ("barsAreHorizontal")]
		bool BarsAreHorizontal { get; set;  }

#if DECIMAL
		[Export ("baseValue")]
		NSDecimal BaseValue { get; set;  }
#endif
		[Export ("plotRange")]
		CPPlotRange PlotRange { get; set;  }

		[Export ("barLabelOffset")]
		float BarLabelOffset { get; set;  }

		[Export ("barLabelTextStyle")]
		CPTextStyle BarLabelTextStyle { get; set;  }

		[Static]
		[Export ("tubularBarPlotWithColor:horizontalBars:")]
		CPBarPlot CreateTubularBarPlot (CPColor color, bool horizontalBars);
	}

	[BaseType (typeof (CPPlotDataSource))]
	[Model]
	interface CPBarPlotDataSource {
		[Export ("barFillForBarPlot:recordIndex:")]
		CPFill GetBarFill (CPBarPlot barPlot, int recordIndex);

		[Export ("barLabelForBarPlot:recordIndex:")]
		CPTextLayer GetBarLabel (CPBarPlot barPlot, int recordIndex);
	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface CPBarPlotDelegate {
		[Abstract]
		[Export ("barPlot:barWasSelectedAtRecordIndex:")]
		void BarSelected (CPBarPlot plot, int recordIndex);
	}
	
	[BaseType (typeof (CPAnnotationHostLayer))]
	interface CPBorderedLayer {
		[Export ("borderLineStyle")]
		CPLineStyle BorderLineStyle { get; set; }

		[Export ("fill")]
		CPFill Fill { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface CPColor {
		[Export ("cgColor")]
		CGColor CGColor { get;  }

		[Static]
		[Export ("clearColor")]
		CPColor ClearColor { get; }

		[Static]
		[Export ("whiteColor")]
		CPColor WhiteColor { get; }

		[Static]
		[Export ("lightGrayColor")]
		CPColor LightGrayColor { get; }

		[Static]
		[Export ("grayColor")]
		CPColor GrayColor { get; }

		[Static]
		[Export ("darkGrayColor")]
		CPColor DarkGrayColor { get; }

		[Static]
		[Export ("blackColor")]
		CPColor BlackColor { get; }

		[Static]
		[Export ("redColor")]
		CPColor RedColor { get; }

		[Static]
		[Export ("greenColor")]
		CPColor GreenColor { get; }

		[Static]
		[Export ("blueColor")]
		CPColor BlueColor { get; }

		[Static]
		[Export ("cyanColor")]
		CPColor CyanColor { get; }

		[Static]
		[Export ("yellowColor")]
		CPColor YellowColor { get; }

		[Static]
		[Export ("magentaColor")]
		CPColor MagentaColor { get; }

		[Static]
		[Export ("orangeColor")]
		CPColor OrangeColor { get; }

		[Static]
		[Export ("purpleColor")]
		CPColor PurpleColor { get; }

		[Static]
		[Export ("brownColor")]
		CPColor BrownColor { get; }

		[Static]
		[Export ("colorWithCGColor:")]
		CPColor FromCGColor (CGColor newCGColor);

		[Static]
		[Export ("colorWithComponentRed:green:blue:alpha:")]
		CPColor FromRgba (float red, float green, float blue, float alpha);

		[Static]
		[Export ("colorWithGenericGray:")]
		CPColor FromGenericGray (float gray);

		[Export ("initWithCGColor:")]
		IntPtr Constructor (CGColor cgColor);

		[Export ("initWithComponentRed:green:blue:alpha:")]
		IntPtr Constructor (float red, float green, float blue, float alpha);

		[Export ("colorWithAlphaComponent:")]
		CPColor ColorWithAlphaComponent (float alpha);
	}

	[BaseType (typeof (NSObject))]
	interface CPColorSpace {
		[Export ("cgColorSpace")]
		CGColorSpace ColorSpace { get; }

		[Static,Export ("genericRGBSpace")]
		CPColorSpace GenericRGBSpace { get; }

		[Export ("initWithCGColorSpace:")]
		IntPtr Constructor (CGColorSpace colorSpace);
	}
		
	[BaseType (typeof (NSObject))]
	interface CPConstrainedPosition {
		[Export ("position")]
		float Position { get; set;  }

		[Export ("lowerBound")]
		float LowerBound { get; set;  }

		[Export ("upperBound")]
		float UpperBound { get; set;  }

		//[Export ("constraints")]
		//CPConstraints Constraints { get; set;  }

		[Export ("initWithPosition:lowerBound:upperBound:")]
		IntPtr Cosntructor (float newPosition, float newLowerBound, float newUpperBound);

		[Export ("initWithAlignment:lowerBound:upperBound:")]
		IntPtr Constructor (CPAlignment newAlignment, float newLowerBound, float newUpperBound);

		[Export ("adjustPositionForOldLowerBound:oldUpperBound:")]
		void AdjustPosition (float oldLowerBound, float oldUpperBound);
	}
	
	[BaseType (typeof (NSObject))]
	interface CPFill {
		[Static]
		[Export ("fillWithColor:")]
		CPFill FromColor (CPColor aColor);

		[Static]
		[Export ("fillWithGradient:")]
		CPFill FromGradient (CPGradient aGradient);

		[Static]
		[Export ("fillWithImage:")]
		CPFill FromImage (CPImage anImage);

		[Export ("initWithColor:")]
		IntPtr Constructor (CPColor aColor);

		[Export ("initWithGradient:")]
		IntPtr Constructor (CPGradient aGradient);

		[Export ("initWithImage:")]
		IntPtr Constructor (CPImage anImage);

		[Export ("fillRect:inContext:")]
		void FillRect (RectangleF theRect, CGContext inContext);

		[Export ("fillPathInContext:")]
		void FillPath (CGContext inContext);
	}

	[BaseType (typeof (NSObject))]
	interface CPGradient {
		[Export ("blendingMode")]
		CPGradientBlendingMode BlendingMode { get;  }

		[Export ("angle")]
		float Angle { get; set;  }

		[Export ("gradientType")]
		CPGradientType GradientType { get; set;  }

		[Static]
		[Export ("gradientWithBeginningColor:endingColor:")]
		CPGradient Create (CPColor beginningColor, CPColor endingColor);

		[Static]
		[Export ("gradientWithBeginningColor:endingColor:beginningPosition:endingPosition:")]
		CPGradient Create  (CPColor beginningColor, CPColor endingColor, float beginningPosition, float endingPosition);

		[Static]
		[Export ("aquaSelectedGradient")]
		CPGradient AquaSelectedGradient { get; }

		[Static]
		[Export ("aquaNormalGradient")]
		CPGradient AquaNormalGradient { get; }

		[Static]
		[Export ("aquaPressedGradient")]
		CPGradient AquaPressedGradient { get; }

		[Static]
		[Export ("unifiedSelectedGradient")]
		CPGradient UnifiedSelectedGradient { get; }

		[Static]
		[Export ("unifiedNormalGradient")]
		CPGradient UnifiedNormalGradient { get; }

		[Static]
		[Export ("unifiedPressedGradient")]
		CPGradient UnifiedPressedGradient { get; }

		[Static]
		[Export ("unifiedDarkGradient")]
		CPGradient UnifiedDarkGradient { get; }

		[Static]
		[Export ("sourceListSelectedGradient")]
		CPGradient SourceListSelectedGradient { get; }

		[Static]
		[Export ("sourceListUnselectedGradient")]
		CPGradient SourceListUnselectedGradient { get; }

		[Static]
		[Export ("rainbowGradient")]
		CPGradient RainbowGradient { get; }

		[Static]
		[Export ("hydrogenSpectrumGradient")]
		CPGradient HydrogenSpectrumGradient { get; }

		[Export ("gradientWithAlphaComponent:")]
		CPGradient GradientWithAlphaComponent (float alpha);

		[Export ("gradientWithBlendingMode:")]
		CPGradient GradientWithBlendingMode (CPGradientBlendingMode mode);

		[Export ("addColorStop:atPosition:")]
		CPGradient AddColorStop (CPColor color, float position);

		[Export ("removeColorStopAtIndex:")]
		CPGradient RemoveColorStop (int index);

		[Export ("removeColorStopAtPosition:")]
		CPGradient RemoveColorStop (float position);

		[Export ("newColorStopAtIndex:")]
		CGColor NewColorStop (int atIndex);

		[Export ("newColorAtPosition:")]
		CGColor NewColorAtPosition (float position);

		[Export ("drawSwatchInRect:inContext:")]
		void DrawSwatch (Rectangle inRectangle, CGContext context);

		[Export ("fillRect:inContext:")]
		void FillRect (RectangleF rectangle, CGContext context);

		[Export ("fillPathInContext:")]
		void FillPath (CGContext inContext);
	}

	[BaseType (typeof (CPBorderedLayer))]
	interface CPGraph {
		[Export ("title")]
		string Title { get; set;  }

		[Export ("titleTextStyle")]
		CPTextStyle TitleTextStyle { get; set;  }

		[Export ("titleDisplacement")]
		PointF TitleDisplacement { get; set;  }

		[Export ("titlePlotAreaFrameAnchor")]
		CPRectAnchor TitlePlotAreaFrameAnchor { get; set;  }

		[Export ("axisSet")]
		CPAxisSet AxisSet { get; set;  }

		[Export ("plotAreaFrame")]
		CPPlotAreaFrame PlotAreaFrame { get; set;  }

		[Export ("defaultPlotSpace")]
		CPPlotSpace DefaultPlotSpace { get;  }

		[Export ("topDownLayerOrder")]
		NSNumber [] TopDownLayerOrder { get; set;  }

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("reloadDataIfNeeded")]
		void ReloadDataIfNeeded ();

		[Export ("allPlots")]
		CPPlot [] AllPlots { get; }

		[Export ("plotAtIndex:")]
		CPPlot PlotAt (int index);

		[Export ("plotWithIdentifier:")]
		CPPlot PlotWithIdentifier (NSObject identifier);

		[Export ("addPlot:")]
		void AddPlot (CPPlot plot);

		[Export ("addPlot:toPlotSpace:")]
		void AddPlot (CPPlot plot, CPPlotSpace toPlotSpace);

		[Export ("removePlot:")]
		void RemovePlot (CPPlot plot);

		[Export ("insertPlot:atIndex:")]
		void InsertPlot (CPPlot plot, int index);

		[Export ("insertPlot:atIndex:intoPlotSpace:")]
		void InsertPlot (CPPlot plot, int index, CPPlotSpace intoPlotSpace);

		[Export ("allPlotSpaces")]
		CPPlotSpace [] PlotSpaces { get; }

		[Export ("plotSpaceAtIndex:")]
		CPPlotSpace PlotSpaceAt (int index);

		[Export ("plotSpaceWithIdentifier:")]
		CPPlotSpace PlotSpaceWithIdentifier (NSObject identifier);

		[Export ("addPlotSpace:")]
		void AddPlotSpace (CPPlotSpace space);

		[Export ("removePlotSpace:")]
		void RemovePlotSpace (CPPlotSpace plotSpace);

		[Export ("applyTheme:")]
		void ApplyTheme (CPTheme theme);

		[Export ("newPlotSpace")]
		CPPlotSpace NewPlotSpace ();

		[Export ("newAxisSet")]
		CPAxisSet NewAxisSet ();
	}
	
	[BaseType (typeof (CPLayer))]
	interface CPGridLineGroup {
		[Export ("plotArea")]
		CPPlotArea PlotArea { get; set;  }

		[Export ("major")]
		bool Major { get; set;  }
	}

	[BaseType (typeof (CPLayer))]
	interface CPGridLines {
		[Export ("axis")]
		CPAxis Axis { get; set;  }

		[Export ("major")]
		bool Major { get; set;  }
	}

	[BaseType (typeof (NSObject))]
	interface CPImage {
		[Export ("image")]
		CGImage Image { get; set;  }

		[Export ("tiled")]
		bool Tiled { [Bind ("isTiled")] get; set;  }

		[Export ("tileAnchoredToContext")]
		bool TileAnchoredToContext { get; set;  }

		[Static]
		[Export ("imageWithCGImage:")]
		CPImage FRomCGImage (CGImage anImage);

		[Static]
		[Export ("imageForPNGFile:")]
		CPImage FromPngFile (string path);

		[Export ("initWithCGImage:")]
		IntPtr Constructor (CGImage anImage);

		[Export ("initForPNGFile:")]
		IntPtr Constructor (string path);

		[Export ("drawInRect:inContext:")]
		void Draw (RectangleF rect, CGContext context);
	}

	[BaseType (typeof (CALayer))]
	interface CPLayer {
		[Export ("CPGraph")]
		CPGraph Graph { get; set;  }

		[Export ("paddingLeft")]
		float PaddingLeft { get; set;  }

		[Export ("paddingTop")]
		float PaddingTop { get; set;  }

		[Export ("paddingRight")]
		float PaddingRight { get; set;  }

		[Export ("paddingBottom")]
		float PaddingBottom { get; set;  }

		[Export ("useFastRendering")]
		bool UseFastRendering { get;  }

		[Export ("masksToBorder")]
		bool MasksToBorder { get; set;  }

		[Export ("outerBorderPath")]
		CGPath OuterBorderPath { get; set;  }

		[Export ("innerBorderPath")]
		CGPath InnerBorderPath { get; set;  }

		[Export ("maskingPath")]
		CGPath Maskingpath { get;  }

		[Export ("sublayerMaskingPath")]
		CGPath SublayerMaskingPath { get;  }

		[Export ("layoutManager")]
		CPLayoutManager LayoutManager { get; set;  }

		[Export ("sublayersExcludedFromAutomaticLayout")]
		NSSet SublayersExcludedFromAutomaticLayout { get;  }

		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF newFrame);

		[Export ("renderAsVectorInContext:")]
		void RenderAsVector (CGContext context);

		[Export ("recursivelyRenderInContext:")]
		void RecursivelyRender (CGContext context);

		[Export ("layoutAndRenderInContext:")]
		void LayoutAndRender (CGContext context);

		[Export ("dataForPDFRepresentationOfLayer")]
		NSData GetPDFRepresentationOfLayer ();

		[Export ("applySublayerMaskToContext:forSublayer:withOffset:")]
		void ApplySublayerMask (CGContext toContext, CPLayer forSublayer, PointF offset);

		[Export ("applyMaskToContext:")]
		void ApplyMaskToContext (CGContext context);

		[Static]
		[Export ("defaultZPosition")]
		float DefaultZPosition { get; }

		[Export ("pixelAlign")]
		void PixelAlign ();

#if MONOTOUCH
		[Export ("uiColor")]
		UIColor Color { get; }

		[Export ("imageOfLayer")]
		UIImage GetImageOfLayer ();

#else
		[Export ("nsColor")]
		NSColor Color { get; }

		[Export ("imageOfLayer")]
		NSImage GetImageOfLayer ();
#endif
		//
		// From CPResponder
		//
		[Export ("pointingDeviceDownEvent:atPoint:")]
		bool PointingDeviceDown (NSObject theEvent, PointF interactionPoint);

		[Export ("pointingDeviceUpEvent:atPoint:")]
		bool PointingDeviceUp (NSObject theEvent, PointF interactionPoint);

		[Export ("pointingDeviceDraggedEvent:atPoint:")]
		bool PointingDeviceDragged (NSObject theEvent, PointF interactionPoint);

		[Export ("pointingDeviceCancelledEvent:")]
		bool PointingDeviceCancelledEvent (NSObject theEvent);
		
	}

	[BaseType (typeof (CPAnnotation))]
	interface CPLayerAnnotation {
		[Export ("anchorLayer")]
		CPLayer AnchorLayer { get; }

		[Export ("rectAnchor")]
		CPRectAnchor RectAnchor { get; set; }

		[Export ("initWithAnchorLayer:")]
		IntPtr Constructor (CPLayer anchorLayer);
	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface CPLayoutManager {
		[Export ("invalidateLayoutOfLayer:")]
		void InvalidateLayoutOfLayer (CALayer layer);

		[Export ("layoutSublayersOfLayer:")]
		void LayoutSublayersOfLayer (CALayer layer);

		[Export ("preferredSizeOfLayer:")]
		SizeF PreferredSizeOfLayer (CALayer layer);

		[Export ("minimumSizeOfLayer:")]
		SizeF MinimumSizeOfLayer (CALayer layer);

		[Export ("maximumSizeOfLayer:")]
		SizeF MaximumSizeOfLayer (CALayer layer);
	}

	[BaseType (typeof (NSObject))]
	interface CPLimitBand {
		[Export ("range")]
		CPPlotRange Range { get; set;  }

		[Export ("fill")]
		CPFill Fill { get; set;  }

		[Static]
		[Export ("limitBandWithRange:fill:")]
		CPLimitBand FromRange (CPPlotRange newRange, CPFill newFill);

		[Export ("initWithRange:fill:")]
		IntPtr Constructor (CPPlotRange newRange, CPFill newFill);
	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface CPLineStyleDelegate {
		[Abstract]
		[Export ("lineStyleDidChange:")]
		void LineStyleDidChange (CPLineStyle lineStyle);
	}

	[BaseType (typeof (NSObject), Delegates=new string [] {"WeakDelegate"}, Events=new Type [] { typeof (CPLineStyleDelegate)})]
	interface CPLineStyle {
		[Export ("delegates"), NullAllowed]
		NSObject WeakDelegate { get; set;  }

		[Wrap ("WeakDelegate")]
		CPLineStyleDelegate Delegate { get; set; }

		[Export ("lineCap")]
		CGLineCap LineCap { get; set;  }

		[Export ("lineJoin")]
		CGLineJoin LineJoin { get; set;  }

		[Export ("miterLimit")]
		float MiterLimit { get; set;  }

		[Export ("lineWidth")]
		float LineWidth { get; set;  }

		[Export ("dashPattern")]
		NSArray DashPattern { get; set;  }

		[Export ("patternPhase")]
		float PatternPhase { get; set;  }

		[Export ("lineColor")]
		CPColor LineColor { get; set;  }

		[Static]
		[Export ("lineStyle")]
		CPLineStyle LineStyle { get; }

		[Export ("setLineStyleInContext:")]
		void SetLineStyleInContext (CGContext theContext);
	}

	[BaseType (typeof (NSObject))]
	interface CPNumericData {
		[Export ("data")]
		NSData Data { get;  }

		[Export ("void")]
		IntPtr Bytes { get;  }

		[Export ("length")]
		int Length { get;  }

		[Export ("dataType")]
		/*CPNumericDataType*/ IntPtr DataType { get;  }

		[Export ("dataTypeFormat")]
		CPDataTypeFormat DataTypeFormat { get;  }

		[Export ("sampleBytes")]
		IntPtr SampleBytes { get;  }

		[Export ("byteOrder")]
		int ByteOrder { get;  }

		[Export ("shape")]
		NSNumber [] Shape { get;  }

		[Export ("numberOfDimensions")]
		int NumberOfDimensions { get;  }

		[Export ("numberOfSamples")]
		int NumberOfSamples { get;  }

		[Static]
		[Export ("numericDataWithData:dataType:shape:")]
		CPNumericData FromData (NSData newData, /*CPNumericDataType*/ IntPtr newDataType, NSNumber [] shapeArray);

		[Static]
		[Export ("numericDataWithData:dataTypeString:shape:")]
		CPNumericData FromData (NSData newData, string newDataTypeString, NSArray shapeArray);

		[Static]
		[Export ("numericDataWithArray:dataType:shape:")]
		CPNumericData FromArray (NSArray newData, /*CPNumericDataType*/ IntPtr newDataType, NSArray shapeArray);

		[Static]
		[Export ("numericDataWithArray:dataTypeString:shape:")]
		CPNumericData FromArray (NSArray newData, string newDataTypeString, NSArray shapeArray);

		[Export ("initWithData:dataType:shape:")]
		IntPtr Constructor (NSData newData, IntPtr /* cpnumericdatatype */ newDataType, NSArray shapeArray);

		[Export ("initWithData:dataTypeString:shape:")]
		IntPtr Constructor (NSData newData, string newDataTypeString, NSArray shapeArray);

		[Export ("initWithArray:dataType:shape:")]
		IntPtr Constructor (NSArray newData, IntPtr /*CPNumericDataType */ newDataType, NSArray shapeArray);

		[Export ("initWithArray:dataTypeString:shape:")]
		IntPtr Constructor (NSArray newData, string newDataTypeString, NSArray shapeArray);

		[Export ("samplePointer:")]
		void SamplePointer (int sample);

		[Export ("sampleValue:")]
		NSNumber SampleValue (int sample);

		[Export ("sampleArray")]
		NSObject [] SampleArray ();
	}

	[BaseType (typeof (CPNumericData))]
	interface CPMutableNumericData {
		[Export ("mutableBytes")]
		IntPtr MutableBytes { get;  }

		[Export ("shape")]
		NSNumber [] Shape { get; set;  }

		[Static]
		[Export ("numericDataWithData:dataType:shape:")]
		CPMutableNumericData FromData (NSData newData, /* CPNumericDataType */ IntPtr newDataType, NSNumber [] shapeArray);

		[Static]
		[Export ("numericDataWithData:dataTypeString:shape:")]
		CPMutableNumericData FromData (NSData newData, string newDataTypeString, NSNumber [] shapeArray);

		[Export ("initWithData:dataType:shape:")]
		IntPtr Constructor (NSData newData, /*CPNumericDataType*/ IntPtr newDataType, NSNumber [] shapeArray);
	}


	[BaseType (typeof (CPPlotDataSource))]
	[Model]
	interface CPPieChartDataSource {
		[Export ("sliceFillForPieChart:recordIndex:")]
		CPFill GetSliceFill (CPPieChart pieChart, int recordIndex);

		[Export ("sliceLabelForPieChart:recordIndex:")]
		CPTextLayer GetSliceLabel (CPPieChart pieChart, int recordIndex);
	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface CPPieChartDelegate {
		[Abstract]
		[Export ("pieChart:sliceWasSelectedAtRecordIndex:")]
		void SliceSelected (CPPieChart plot, int recordIndex);

	}

	[BaseType (typeof (CPPlot))]
	interface CPPieChart {
		[Export ("pieRadius")]
		float PieRadius { get; set;  }

		[Export ("sliceLabelOffset")]
		float SliceLabelOffset { get; set;  }

		[Export ("startAngle")]
		float StartAngle { get; set;  }

		[Export ("sliceDirection")]
		CPPieDirection SliceDirection { get; set;  }

		[Export ("centerAnchor")]
		PointF CenterAnchor { get; set;  }

		[Export ("borderLineStyle")]
		CPLineStyle BorderLineStyle { get; set;  }

		[Static]
		[Export ("defaultPieSliceColorForIndex:")]
		CPColor DefaultPieSliceColorForIndex (int pieSliceIndex);
	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface CPPlotDataSource {
		[Abstract]
		[Export ("numberOfRecordsForPlot:")]
		int NumberOfRecordsForPlot (CPPlot plot);

		[Export ("numbersForPlot:field:recordIndexRange:")]
		NSNumber [] NumbersForPlot (CPPlot forPlot, CPPlotField forFieldEnum, NSRange indexRange);

		[Export ("numberForPlot:field:recordIndex:")]
		NSNumber NumberForPlot (CPPlot plot, CPPlotField forFieldEnum, int index);

		[Export ("doublesForPlot:field:recordIndexRange:")]
		IntPtr DoublesForPlot (CPPlot plot, CPPlotField forFieldEnum, NSRange indexRange);

		[Export ("doubleForPlot:field:recordIndex:")]
		double DoubleForPlot (CPPlot plot, CPPlotField forFieldEnum, int index);

		[Export ("dataForPlot:field:recordIndexRange:")]
		CPNumericData DataForPlot (CPPlot plot, CPPlotField forFieldEnum, NSRange indexRange);

		[Export ("recordIndexRangeForPlot:plotRange:")]
		NSRange RecordIndexRange (CPPlot forPlot, CPPlotRange plotRange);

		[Export ("dataLabelForPlot:recordIndex:")]
		CPLayer DataLabelForPlot (CPPlot plot, int recordIndex);
	}

	[BaseType (typeof (CPAnnotationHostLayer))]
	interface CPPlot {
		[Export ("dataSource"), NullAllowed]
		CPPlotDataSource DataSource { get; set;  }

		[Export ("identifier")]
		NSObject Identifier { get; set;  }

		[Export ("plotSpace")]
		CPPlotSpace PlotSpace { get; set;  }

		[Export ("plotArea")]
		CPPlotArea PlotArea { get;  }

		[Export ("dataNeedsReloading")]
		bool DataNeedsReloading { get;  }

		[Export ("cachedDataCount")]
		int CachedDataCount { get;  }

		[Export ("doublePrecisionCache")]
		bool DoublePrecisionCache { get;  }

		[Export ("cachePrecision")]
		CPPlotCachePrecision CachePrecision { get; set;  }

		[Export ("doubleDataType")]
		/*CPNumericDataType*/ IntPtr DoubleDataType { get;  }

		[Export ("decimalDataType")]
		/* CPNumericDataType*/ IntPtr DecimalDataType { get;  }

		[Export ("needsRelabel")]
		bool NeedsRelabel { get;  }

		[Export ("labelOffset")]
		float LabelOffset { get; set;  }

		[Export ("labelRotation")]
		float LabelRotation { get; set;  }

		[Export ("labelField")]
		int LabelField { get; set;  }

		[Export ("labelTextStyle")]
		CPTextStyle LabelTextStyle { get; set;  }

		[Export ("labelFormatter")]
		NSObject /* NSNumberFormatter */ LabelFormatter { get; set;  }


		[Export ("setNeedsRelabel")]
		void SetNeedsRelabel ();

		[Export ("relabel")]
		void Relabel ();

		[Export ("relabelIndexRange:")]
		void RelabelIndexRange (NSRange indexRange);

		[Export ("setDataNeedsReloading")]
		void SetDataNeedsReloading ();

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("reloadDataIfNeeded")]
		void ReloadDataIfNeeded ();

		[Export ("reloadDataInIndexRange:")]
		void ReloadDataInIndexRange (NSRange indexRange);

		[Export ("insertDataAtIndex:numberOfRecords:")]
		void InsertData (int index, int numberOfRecords);

		[Export ("deleteDataInIndexRange:")]
		void DeleteData (NSRange indexRange);

		[Export ("numbersFromDataSourceForField:recordIndexRange:")]
		NSObject NumbersFromDataSource (CPPlotField forFieldEnum, NSRange indexRange);

		[Export ("cachedNumbersForField:")]
		CPMutableNumericData CachedNumbersForField (CPPlotField forFieldEnum);

		[Export ("cachedNumberForField:recordIndex:")]
		NSNumber CachedNumberForField (CPPlotField forFieldEnum, int index);

		[Export ("cachedDoubleForField:recordIndex:")]
		double CachedDoubleForField (CPPlotField forFieldEnum, int index);

#if DECIMAL
		[Export ("cachedDecimalForField:recordIndex:")]
		NSDecimal CachedDecimalForField (CPPlotField forFieldEnum, int index);
#endif

		[Export ("cacheNumbers:forField:")]
		void CacheNumbers (NSObject numbers, CPPlotField forFieldEnum);

		[Export ("cacheNumbers:forField:atRecordIndex:")]
		void CacheNumbers (NSObject numbers, CPPlotField forFieldEnum, int index);

		[Export ("plotRangeForField:")]
		CPPlotRange PlotRangeForField (CPPlotField forFieldEnum);

		[Export ("plotRangeForCoordinate:")]
		CPPlotRange PlotRangeForCoordinate (CPCoordinate coord);

		[Export ("numberOfFields")]
		int NumberOfFields ();

		[Export ("fieldIdentifiers")]
		NSObject [] FieldIdentifiers ();

		[Export ("fieldIdentifiersForCoordinate:")]
		NSObject [] FieldIdentifiersForCoordinate (CPCoordinate coord);

		[Export ("positionLabelAnnotation:forIndex:")]
		void PositionLabelAnnotationforIndex (CPPlotSpaceAnnotation label, int index);
	}

	[BaseType (typeof (CPAnnotationHostLayer))]
	interface CPPlotArea {
		[Export ("minorGridLineGroup")]
		CPGridLineGroup MinorGridLineGroup { get; set;  }

		[Export ("majorGridLineGroup")]
		CPGridLineGroup MajorGridLineGroup { get; set;  }

		[Export ("axisSet")]
		CPAxisSet AxisSet { get; set;  }

		[Export ("plotGroup")]
		CPPlotGroup PlotGroup { get; set;  }

		[Export ("axisLabelGroup")]
		CPAxisLabelGroup AxisLabelGroup { get; set;  }

		[Export ("axisTitleGroup")]
		CPAxisLabelGroup AxisTitleGroup { get; set;  }

		[Export ("topDownLayerOrder")]
		NSNumber [] TopDownLayerOrder { get; set;  }

		[Export ("borderLineStyle")]
		CPLineStyle BorderLineStyle { get; set;  }

		[Export ("fill")]
		CPFill Fill { get; set;  }

		[Export ("updateAxisSetLayersForType:")]
		void UpdateAxisSetLayers (CPGraphLayerType forLayerType);

		[Export ("setAxisSetLayersForType:")]
		void SetAxisSetLayers (CPGraphLayerType forLayerType);

		[Export ("sublayerIndexForAxis:layerType:")]
		int SublayerIndex (CPAxis forAxis, CPGraphLayerType layerType);
	}

	[BaseType (typeof (CPBorderedLayer))]
	interface CPPlotAreaFrame {
		[Export ("plotArea")]
		CPPlotArea PlotArea { get;  }

		[Export ("axisSet")]
		CPAxisSet AxisSet { get; set;  }

		[Export ("plotGroup")]
		CPPlotGroup PlotGroup { get; set;  }
	}


	[BaseType (typeof (CPLayer))]
	interface CPPlotGroup {
		[Export ("identifier")]
		NSObject Identifier { get; set;  }

		[Export ("addPlot:")]
		void AddPlot (CPPlot plot);

		[Export ("removePlot:")]
		void RemovePlot (CPPlot plot);

	}

	[BaseType (typeof (NSObject))]
	interface CPPlotRange {
#if DECIMAL
		[Export ("location")]
		NSDecimal Location { get; set;  }

		[Export ("length")]
		NSDecimal Length { get; set;  }

		[Export ("end")]
		NSDecimal End { get;  }

		[Export ("minLimit")]
		NSDecimal MinLimit { get;  }

		[Export ("maxLimit")]
		NSDecimal MaxLimit { get;  }

		[Export ("expandRangeByFactor:")]
		void ExpandRangeByFactor (NSDecimal factor);

		[Static]
		[Export ("plotRangeWithLocation:length:")]
		CPPlotRange FromLocationAndLength (NSDecimal loc, NSDecimal len);

		[Export ("initWithLocation:length:")]
		IntPtr Constructor (NSDecimal loc, NSDecimal len);

		[Export ("contains:")]
		bool Contains (NSDecimal number);

		[Export ("compareToDecimal:")]
		CPPlotRangeComparisonResult CompareToDecimal (NSDecimal number);
#endif
		[Export ("locationDouble")]
		double LocationDouble { get;  }

		[Export ("lengthDouble")]
		double LengthDouble { get;  }

		[Export ("endDouble")]
		double EndDouble { get;  }

		[Export ("minLimitDouble")]
		double MinLimitDouble { get;  }

		[Export ("maxLimitDouble")]
		double MaxLimitDouble { get;  }

		[Export ("containsDouble:")]
		bool ContainsDouble (double number);

		[Export ("isEqualToRange:")]
		bool IsEqualToRange (CPPlotRange otherRange);

		[Export ("unionPlotRange:")]
		void UnionPlotRange (CPPlotRange otherRange);

		[Export ("intersectionPlotRange:")]
		void IntersectionPlotRange (CPPlotRange otherRange);

		[Export ("shiftLocationToFitInRange:")]
		void ShiftLocationToFitInRange (CPPlotRange otherRange);

		[Export ("shiftEndToFitInRange:")]
		void ShiftEndToFitInRange (CPPlotRange otherRange);

		[Export ("compareToNumber:")]
		CPPlotRangeComparisonResult CompareToNumber (NSNumber number);

		[Export ("compareToDouble:")]
		CPPlotRangeComparisonResult CompareToDouble (double number);
	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface CPPlotSpaceDelegate {
		[Abstract, DelegateName ("CPEventPointPredicate"), DefaultValue (false)]
		[Export ("plotSpace:shouldHandlePointingDeviceDownEvent:atPoint:")]
		bool ShouldHandlePointingDeviceDownEvent (CPPlotSpace space, NSObject evt, PointF point);

		[Abstract, DelegateName ("CPEventPointPredicate"), DefaultValue (false)]
		[Export ("plotSpace:shouldHandlePointingDeviceDraggedEvent:atPoint:")]
		bool ShouldHandlePointingDeviceDraggedEvent (CPPlotSpace space, NSObject evt, PointF point);

		[Abstract, DelegateName ("CPEventPredicate"), DefaultValue (false)]
		[Export ("plotSpace:shouldHandlePointingDeviceCancelledEvent:")]
		bool ShouldHandlePointingDeviceCancelledEvent (CPPlotSpace space, NSObject evt);

		[Abstract, DelegateName ("CPEventPointPredicate"), DefaultValue (false)]
		[Export ("plotSpace:shouldHandlePointingDeviceUpEvent:atPoint:")]
		bool ShouldHandlePointingDeviceUpEvent (CPPlotSpace space, NSObject evt, PointF atPoint);
		
		[Abstract, DelegateName ("CPDisplacement"), DefaultValueFromArgument ("proposedDisplacementVector")]
		[Export ("plotSpace:willDisplaceBy:")]
		PointF WillDisplaceBy (CPPlotSpace space, PointF proposedDisplacementVector);

		[Abstract, DelegateName ("CPWillChangePlotRange"), DefaultValueFromArgument ("toNewRange")]
		[Export ("plotSpace:willChangePlotRangeTo:forCoordinate:")]
		CPPlotRange WillChangePlotRange (CPPlotSpace space, CPPlotRange toNewRange, CPCoordinate forCoordinate);

		[Abstract, EventArgs ("CPPlotChanged")]
		[Export ("plotSpace:didChangePlotRangeForCoordinate:")]
		void DidChangePlotRange (CPPlotSpace space, CPCoordinate forCoordinate);
	}

	[BaseType (typeof (NSObject), Delegates=new string [] { "WeakDelegate" }, Events= new Type [] { typeof (CPPlotSpaceDelegate) })]
	interface CPPlotSpace {
		[Export ("identifier")]
		NSObject Identifier { get; set;  }

		[Export ("allowsUserInteraction")]
		bool AllowsUserInteraction { get; set;  }

		[Export ("graph")]
		CPGraph Graph { get; set;  }

		[Export ("delegate"), NullAllowed]
		NSObject WeakDelegate { get; set;  }

		[Wrap ("WeakDelegate")]
		CPPlotSpaceDelegate Delegate { get; set;  }

		[Export ("plotAreaViewPointForDoublePrecisionPlotPoint:")]
		PointF PlotAreaViewPoint (double plotPoint);

#if DECIMAL
		[Abstract, Export ("plotAreaViewPointForPlotPoint:")]
		PointF PlotAreaViewPoint (NSDecimal forPlotPoint);

		[Export ("plotPoint:forPlotAreaViewPoint:")]
		void PlotPointforPlotAreaViewPoint (ref NSDecimal plotPoint, PointF plotAreaViewPoint);
#endif
		[Export ("doublePrecisionPlotPoint:forPlotAreaViewPoint:")]
		void PlotPoint (ref double plotPoint, PointF plotAreaViewPoint);

		[Export ("setPlotRange:forCoordinate:")]
		void SetPlotRange (CPPlotRange newRange, CPCoordinate coordinate);

		[Export ("plotRangeForCoordinate:")]
		CPPlotRange GetPlotRange (CPCoordinate coordinate);

		[Export ("scaleToFitPlots:")]
		void ScaleToFitPlots (CPPlot [] plots);

		//
		// From CPResponder
		//
		[Export ("pointingDeviceDownEvent:atPoint:")]
		bool PointingDeviceDown (NSObject theEvent, PointF interactionPoint);

		[Export ("pointingDeviceUpEvent:atPoint:")]
		bool PointingDeviceUp (NSObject theEvent, PointF interactionPoint);

		[Export ("pointingDeviceDraggedEvent:atPoint:")]
		bool PointingDeviceDragged (NSObject theEvent, PointF interactionPoint);

		[Export ("pointingDeviceCancelledEvent:")]
		bool PointingDeviceCancelledEvent (NSObject theEvent);
	}


	[BaseType (typeof (CPPlotSpace))]
	interface CPXYPlotSpace {
		[Export ("xRange")]
		CPPlotRange XRange { get; set;  }

		[Export ("yRange")]
		CPPlotRange YRange { get; set;  }

		[Export ("globalXRange")]
		CPPlotRange GlobalXRange { get; set;  }

		[Export ("globalYRange")]
		CPPlotRange GlobalYRange { get; set;  }

		[Export ("xScaleType")]
		CPScaleType XScaleType { get; set;  }

		[Export ("yScaleType")]
		CPScaleType YScaleType { get; set;  }
	}

	[BaseType (typeof (CPAnnotation))]
	interface CPPlotSpaceAnnotation {
		// Decimal numbers
		[Export ("anchorPlotPoint")]
		NSNumber [] AnchorPlotPoint { get; set;  }

		[Export ("plotSpace")]
		CPPlotSpace PlotSpace { get;  }

		[Export ("initWithPlotSpace:anchorPlotPoint:")]
		IntPtr Constructor (CPPlotSpace space, NSNumber [] plotPoint);
	}


	[BaseType (typeof (NSObject))]
	interface CPPlotSymbol {
		[Export ("size")]
		SizeF Size { get; set;  }

		[Export ("symbolType")]
		CPPlotSymbolType SymbolType { get; set;  }

		[Export ("lineStyle")]
		CPLineStyle LineStyle { get; set;  }

		[Export ("fill")]
		CPFill Fill { get; set;  }

		[Export ("customSymbolPath")]
		CGPath CustomSymbolPath { get; set;  }

		[Export ("usesEvenOddClipRule")]
		bool UsesEvenOddClipRule { get; set;  }

		[Static]
		[Export ("plotSymbol")]
		CPPlotSymbol PlotSymbol { get; }

		[Static]
		[Export ("crossPlotSymbol")]
		CPPlotSymbol CrossPlotSymbol { get; }

		[Static]
		[Export ("ellipsePlotSymbol")]
		CPPlotSymbol EllipsePlotSymbol { get; }

		[Static]
		[Export ("rectanglePlotSymbol")]
		CPPlotSymbol RectanglePlotSymbol { get; }

		[Static]
		[Export ("plusPlotSymbol")]
		CPPlotSymbol PlusPlotSymbol { get; }

		[Static]
		[Export ("starPlotSymbol")]
		CPPlotSymbol StarPlotSymbol { get; }

		[Static]
		[Export ("diamondPlotSymbol")]
		CPPlotSymbol DiamondPlotSymbol { get; }

		[Static]
		[Export ("trianglePlotSymbol")]
		CPPlotSymbol TrianglePlotSymbol { get; }

		[Static]
		[Export ("pentagonPlotSymbol")]
		CPPlotSymbol PentagonPlotSymbol { get; }

		[Static]
		[Export ("hexagonPlotSymbol")]
		CPPlotSymbol HexagonPlotSymbol { get; }

		[Static]
		[Export ("dashPlotSymbol")]
		CPPlotSymbol DashPlotSymbol { get; }

		[Static]
		[Export ("snowPlotSymbol")]
		CPPlotSymbol SnowPlotSymbol { get; }

		[Static]
		[Export ("customPlotSymbolWithPath:")]
		CPPlotSymbol CustomPlotSymbolFromPath (CGPath Path);

		[Export ("renderInContext:atPoint:")]
		void RenderInContext (CGContext inContext, PointF centerPoint);

		[Export ("renderAsVectorInContext:atPoint:")]
		void RenderAsVector (CGContext inContext, PointF centerPoint);
	}

	[BaseType (typeof (CPLayer))]
	interface CPTextLayer {
		[Export ("text")]
		string Text { get; set;  }

		[Export ("textStyle")]
		CPTextStyle TextStyle { get; set;  }

		[Export ("initWithText:")]
		IntPtr Constructor (string newText);

		[Export ("initWithText:style:")]
		IntPtr Constructor (string newText, CPTextStyle newStyle);

		[Export ("sizeToFit")]
		void SizeToFit ();

	}
	
	[BaseType (typeof (NSObject))]
	[Model]
	interface CPTextStyleDelegate {
		[Abstract]
		[Export ("textStyleDidChange:")]
		void TextStyleDidChange (CPTextStyle textStyle);
	}

	[BaseType (typeof (NSObject), Delegates=new string [] {"WeakDelegate"}, Events=new Type [] { typeof (CPTextStyleDelegate) })]
	interface CPTextStyle {
		[Export ("delegate"), NullAllowedAttribute]
		NSObject WeakDelegate { get; set;  }

		[Wrap ("WeakDelegate")]
		CPTextStyleDelegate Delegate { get; set; }

		[Export ("fontName")]
		string FontName { get; set;  }

		[Export ("fontSize")]
		float FontSize { get; set;  }

		[Export ("color")]
		CPColor Color { get; set;  }

		[Static]
		[Export ("textStyle")]
		CPTextStyle CreateTextStyle ();

		[Export ("sizeWithTextStyle:")]
		SizeF SizeWithTextStyle (CPTextStyle style);

		[Export ("drawAtPoint:withTextStyle:inContext:")]
		void DrawAtPointwithTextStyleinContext (PointF point, CPTextStyle style, CGContext context);
	}

	[BaseType (typeof (NSObject))]
	interface CPTheme {
		[Export ("name")]
		string Name { get; set;  }

		[Export ("graphClass")]
		Class GraphClass { get; set;  }

		[Static]
		[Export ("themeClasses")]
		NSArray ThemeClasses { get; }

		[Static]
		[Export ("themeNamed:")]
		CPTheme ThemeNamed (string theme);

		[Static]
		[Export ("addTheme:")]
		void AddTheme (CPTheme newTheme);

		[Static]
		[Export ("defaultName")]
		string DefaultName { get; }

		[Export ("applyThemeToGraph:")]
		void ApplyThemeToGraph (CPGraph graph);

		[Export ("newGraph")]
		NSObject NewGraph ();

		[Export ("applyThemeToBackground:")]
		void ApplyThemeToBackground (CPGraph graph);

		[Export ("applyThemeToPlotArea:")]
		void ApplyThemeToPlotArea (CPPlotAreaFrame plotAreaFrame);

		[Export ("applyThemeToAxisSet:")]
		void ApplyThemeToAxisSet (CPAxisSet axisSet);

#if false
		[Field ("kCPDarkGradientTheme", "__Internal")]
		NSString DarkGradientTheme { get; }

		[Field ("kCPPlainWhiteTheme", "__Internal")]
		NSString PlainWhiteTheme { get; }
		
		[Field ("kCPPlainBlackTheme", "__Internal")]
		NSString PlainBlackTheme { get; }
		
		[Field ("kCPSlateTheme", "__Internal")]
		NSString SlateTheme { get; }
		
		[Field ("kCPStocksTheme", "__Internal")]
		NSString StocksTheme { get; }
#endif
	}

#if false
	// missing: NSNumberFormatter
	[BaseType (typeof (NSNumberFormatter))]
	interface CPTimeFormatter {
		[Export ("dateFormatter")]
		NSDateFormatter DateFormatter { get; set; }

		[Export ("referenceDate")]
		NSDate Referencedate { get; set; }

		[Export ("initWithDateFormatter:")]
		IntPtr Constructor (NSDateFormatter reference);
	}
#endif

	[BaseType (typeof (CPPlot))]
	interface CPTradingRangePlot {
		[Export ("lineStyle")]
		CPLineStyle LineStyle { get; set;  }

		[Export ("increaseFill")]
		CPFill IncreaseFill { get; set;  }

		[Export ("decreaseFill")]
		CPFill DecreaseFill { get; set;  }

		[Export ("plotStyle")]
		CPTradingRangePlotStyle PlotStyle { get; set;  }

		[Export ("barWidth")]
		float BarWidth { get; set;  }

		[Export ("stickLength")]
		float StickLength { get; set;  }

		[Export ("barCornerRadius")]
		float BarCornerRadius { get; set;  }

	}

	[BaseType (typeof (CPTheme))]
	interface CPXYTheme {
	}
	
	[BaseType (typeof (CPXYTheme))]
	interface CPDarkGradientTheme {
	}

	[BaseType (typeof (CPXYTheme))]
	interface CPPlainBlackTheme {
	}

	[BaseType (typeof (CPXYTheme))]
	interface CPPlainWhiteTheme {
	}

	[BaseType (typeof (CPXYTheme))]
	interface CPSlateTheme {
	}

	[BaseType (typeof (CPXYTheme))]
	interface CPStocksTheme {
	}

	[BaseType (typeof (CPPlotSpace))]
	interface CPPolarPlotSpace {
	}

	[BaseType (typeof (CPGraph))]
	interface CPXYGraph {
		[Export ("initWithFrame:xScaleType:yScaleType:")]
		IntPtr Constructor (RectangleF newFrame, CPScaleType newXScale, CPScaleType newYScale);
	}
	
	[BaseType (typeof (CPGraph))]
	interface CPDerivedXYGraph : CPXYGraph {
	}
	
	[BaseType (typeof (CPGraph))]
	interface CPGraphXY {
		[Export ("initWithFrame:xScaleType:yScaleType:")]
		IntPtr Constructor (RectangleF frame, CPScaleType xScaleType, CPScaleType yScaleType);
	}

	[BaseType (typeof (CPAxis))]
	interface CPXYAxis {
#if DECIMAL
		[Export ("orthogonalCoordinateDecimal")]
		NSDecimal OrthogonalCoordinateDecimal { get; set;  }
#endif
//[Export ("constraints")]
//CPConstraints Constraints { get; set;  }

		[Export ("isFloatingAxis")]
		bool IsFloatingAxis { get; set;  }
	}

	[BaseType (typeof (CPAxisSet))]
	interface CPXYAxisSet  : CPAxisSet {
		[Export ("xAxis")]
		CPXYAxis XAxis { get; }

		[Export ("yAxis")]
		CPXYAxis YAxis { get; }
	}
	

	[BaseType (typeof (CPPlotDataSource))]
	[Model]
	interface CPScatterPlotDataSource {
		[Export ("symbolsForScatterPlot:recordIndexRange:")]
		CPPlotSymbol [] GetSymbols (CPScatterPlot plot, NSRange indexRange);

		[Export ("symbolForScatterPlot:recordIndex:")]
		CPPlotSymbol GetSymbol (CPScatterPlot plot, int recordIndex);

	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface CPScatterPlotDelegate {
		[Abstract]
		[Export ("scatterPlot:plotSymbolWasSelectedAtRecordIndex:")]
		void PlotSymbolSelected (CPScatterPlot plot, int recordIndex);
	}

	[BaseType (typeof (CPPlot))]
	interface CPScatterPlot {
		[Export ("dataLineStyle")]
		CPLineStyle DataLineStyle { get; set;  }

		[Export ("plotSymbol")]
		CPPlotSymbol PlotSymbol { get; set;  }

		[Export ("areaFill")]
		CPFill AreaFill { get; set;  }

		[Export ("areaFill2")]
		CPFill AreaFill2 { get; set;  }

#if DECIMAL
		[Export ("areaBaseValue")]
		NSDecimal AreaBaseValue { get; set;  }

		[Export ("areaBaseValue2")]
		NSDecimal AreaBaseValue2 { get; set;  }
#endif

		[Export ("interpolation")]
		CPScatterPlotInterpolation Interpolation { get; set;  }

		[Export ("plotSymbolMarginForHitDetection")]
		float PlotSymbolMarginForHitDetection { get; set;  }

		[Export ("indexOfVisiblePointClosestToPlotAreaPoint:")]
		int IndexOfVisiblePointClosestToPlotAreaPoint (PointF viewPoint);

		[Export ("plotAreaPointOfVisiblePointAtIndex:")]
		PointF PlotAreaPointOfVisiblePointAtIndex (int index);

		[Export ("plotSymbolForRecordIndex:")]
		CPPlotSymbol PlotSymbolForRecordIndex (int index);
	}

#if MONOTOUCH
	[BaseType (typeof (UIView))]
	interface CPGraphHostingView {
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF frame);

		[Export ("hostedGraph")]
		CPGraph HostedGraph { get; set; }

		[Export ("collapsesLayers")]
		bool CollapsesLayers { get; set; }
				
	}
#else
	[BaseType (typeof (NSView))]
	interface CPGraphHostingView {
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF frame);

		[Export ("hostedLayer")]
		CPLayer HostedLayer { get; set; }
	}
#endif
}