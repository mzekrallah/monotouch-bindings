using System;

#if __UNIFIED__
using ObjCRuntime;
#else
using MonoTouch.ObjCRuntime;
#endif

[assembly: LinkWith ("libAudioKit.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64 | LinkTarget.Simulator | LinkTarget.Simulator64, Frameworks = "AVFoundation AudioToolbox CoreMIDI", LinkerFlags = "-ObjC", SmartLink = true, ForceLoad = true)]