using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace UserModule_BARCO_PROJECTOR_F_SERIES_PROCESSOR
{
    public class UserModuleClass_BARCO_PROJECTOR_F_SERIES_PROCESSOR : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput POLL;
        Crestron.Logos.SplusObjects.DigitalInput POLL_PICTURE;
        Crestron.Logos.SplusObjects.AnalogInput INPUT_VALUE;
        Crestron.Logos.SplusObjects.AnalogInput ASPECT_VALUE;
        Crestron.Logos.SplusObjects.AnalogInput GAMMA_VALUE;
        Crestron.Logos.SplusObjects.AnalogInput CONTRAST_IN;
        Crestron.Logos.SplusObjects.AnalogInput BRIGHTNESS_IN;
        Crestron.Logos.SplusObjects.AnalogInput SATURATION_IN;
        Crestron.Logos.SplusObjects.BufferInput QUEUE__DOLLAR__;
        Crestron.Logos.SplusObjects.BufferInput FROM_DEVICE__DOLLAR__;
        Crestron.Logos.SplusObjects.AnalogOutput POWER_STATUS;
        Crestron.Logos.SplusObjects.AnalogOutput INPUT_STATUS;
        Crestron.Logos.SplusObjects.AnalogOutput PICTUREMUTE_STATUS;
        Crestron.Logos.SplusObjects.AnalogOutput FREEZE_STATUS;
        Crestron.Logos.SplusObjects.AnalogOutput ASPECT_STATUS;
        Crestron.Logos.SplusObjects.AnalogOutput GAMMA_STATUS;
        Crestron.Logos.SplusObjects.AnalogOutput BRIGHTNESS_LEVEL;
        Crestron.Logos.SplusObjects.AnalogOutput SATURATION_LEVEL;
        Crestron.Logos.SplusObjects.AnalogOutput CONTRAST_LEVEL;
        Crestron.Logos.SplusObjects.AnalogOutput LAMP_HOURS;
        Crestron.Logos.SplusObjects.StringOutput TO_DEVICE__DOLLAR__;
        StringParameter ADDRESS;
        object POLL_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 59;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (POWER_STATUS  .Value == 3))  ) ) 
                    { 
                    __context__.SourceCodeLine = 62;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.GISTORE != _SplusNVRAM.GINEXT))  ) ) 
                        { 
                        __context__.SourceCodeLine = 64;
                        TO_DEVICE__DOLLAR__  .UpdateValue ( _SplusNVRAM.CMD_QUE [ _SplusNVRAM.GINEXT ]  ) ; 
                        __context__.SourceCodeLine = 65;
                        _SplusNVRAM.GINEXT = (ushort) ( (_SplusNVRAM.GINEXT + 1) ) ; 
                        __context__.SourceCodeLine = 66;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.GINEXT > 20 ))  ) ) 
                            {
                            __context__.SourceCodeLine = 67;
                            _SplusNVRAM.GINEXT = (ushort) ( 1 ) ; 
                            }
                        
                        __context__.SourceCodeLine = 69;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.GINEXT == _SplusNVRAM.GISTORE))  ) ) 
                            {
                            __context__.SourceCodeLine = 70;
                            _SplusNVRAM.GITOSEND = (ushort) ( 0 ) ; 
                            }
                        
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 72;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (POLL_PICTURE  .Value == 0))  ) ) 
                            { 
                            __context__.SourceCodeLine = 74;
                            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.GIPOLLNEXT != 5))  ) ) 
                                { 
                                __context__.SourceCodeLine = 76;
                                TO_DEVICE__DOLLAR__  .UpdateValue ( ":" + ADDRESS + " " + _SplusNVRAM.GSPOLLCMD [ _SplusNVRAM.GIPOLLNEXT ] + "\r"  ) ; 
                                __context__.SourceCodeLine = 77;
                                _SplusNVRAM.GIPOLLNEXT = (ushort) ( (_SplusNVRAM.GIPOLLNEXT + 1) ) ; 
                                } 
                            
                            else 
                                { 
                                __context__.SourceCodeLine = 81;
                                _SplusNVRAM.GIPOLLNEXT = (ushort) ( 1 ) ; 
                                __context__.SourceCodeLine = 82;
                                TO_DEVICE__DOLLAR__  .UpdateValue ( ":" + ADDRESS + " " + _SplusNVRAM.GSPOLLCMD [ _SplusNVRAM.GIPOLLNEXT ] + "\r"  ) ; 
                                __context__.SourceCodeLine = 83;
                                _SplusNVRAM.GIPOLLNEXT = (ushort) ( (_SplusNVRAM.GIPOLLNEXT + 1) ) ; 
                                } 
                            
                            } 
                        
                        else 
                            { 
                            __context__.SourceCodeLine = 89;
                            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.GIPOLLNEXT != 5))  ) ) 
                                { 
                                __context__.SourceCodeLine = 91;
                                TO_DEVICE__DOLLAR__  .UpdateValue ( ":" + ADDRESS + " " + _SplusNVRAM.GSPOLLCMD [ _SplusNVRAM.GIPOLLNEXT ] + "\r"  ) ; 
                                __context__.SourceCodeLine = 92;
                                _SplusNVRAM.GIPOLLNEXT = (ushort) ( (_SplusNVRAM.GIPOLLNEXT + 1) ) ; 
                                } 
                            
                            else 
                                {
                                __context__.SourceCodeLine = 94;
                                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.GIPOLL_PICTURE != 4))  ) ) 
                                    { 
                                    __context__.SourceCodeLine = 96;
                                    TO_DEVICE__DOLLAR__  .UpdateValue ( ":" + ADDRESS + " " + _SplusNVRAM.GSPOLL_PICTURE [ _SplusNVRAM.GIPOLL_PICTURE ] + "\r"  ) ; 
                                    __context__.SourceCodeLine = 97;
                                    _SplusNVRAM.GIPOLL_PICTURE = (ushort) ( (_SplusNVRAM.GIPOLL_PICTURE + 1) ) ; 
                                    } 
                                
                                else 
                                    { 
                                    __context__.SourceCodeLine = 101;
                                    _SplusNVRAM.GIPOLLNEXT = (ushort) ( 1 ) ; 
                                    __context__.SourceCodeLine = 102;
                                    TO_DEVICE__DOLLAR__  .UpdateValue ( ":" + ADDRESS + " " + _SplusNVRAM.GSPOLLCMD [ _SplusNVRAM.GIPOLLNEXT ] + "\r"  ) ; 
                                    __context__.SourceCodeLine = 103;
                                    _SplusNVRAM.GIPOLLNEXT = (ushort) ( (_SplusNVRAM.GIPOLLNEXT + 1) ) ; 
                                    __context__.SourceCodeLine = 104;
                                    _SplusNVRAM.GIPOLL_PICTURE = (ushort) ( 1 ) ; 
                                    } 
                                
                                }
                            
                            } 
                        
                        }
                    
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 110;
                    TO_DEVICE__DOLLAR__  .UpdateValue ( ":" + ADDRESS + " " + _SplusNVRAM.GSPOLLCMD [ 1 ] + "\r"  ) ; 
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object INPUT_VALUE_OnChange_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            CrestronString INCMD;
            INCMD  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
            
            
            __context__.SourceCodeLine = 120;
            INCMD  .UpdateValue ( ": " + ADDRESS + " " + "IABS" + Functions.ItoA (  (int) ( INPUT_VALUE  .UshortValue ) ) + "\r"  ) ; 
            __context__.SourceCodeLine = 122;
            _SplusNVRAM.CMD_QUE [ _SplusNVRAM.GISTORE ]  .UpdateValue ( INCMD  ) ; 
            __context__.SourceCodeLine = 124;
            _SplusNVRAM.GISTORE = (ushort) ( (_SplusNVRAM.GISTORE + 1) ) ; 
            __context__.SourceCodeLine = 125;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.GISTORE > 20 ))  ) ) 
                { 
                __context__.SourceCodeLine = 127;
                _SplusNVRAM.GISTORE = (ushort) ( 1 ) ; 
                } 
            
            __context__.SourceCodeLine = 130;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.GISTORE != _SplusNVRAM.GINEXT))  ) ) 
                { 
                __context__.SourceCodeLine = 132;
                _SplusNVRAM.GITOSEND = (ushort) ( 1 ) ; 
                } 
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object ASPECT_VALUE_OnChange_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        CrestronString INCMD;
        INCMD  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
        
        
        __context__.SourceCodeLine = 140;
        INCMD  .UpdateValue ( ": " + ADDRESS + " " + "SABS" + Functions.ItoA (  (int) ( ASPECT_VALUE  .UshortValue ) ) + "\r"  ) ; 
        __context__.SourceCodeLine = 142;
        _SplusNVRAM.CMD_QUE [ _SplusNVRAM.GISTORE ]  .UpdateValue ( INCMD  ) ; 
        __context__.SourceCodeLine = 144;
        _SplusNVRAM.GISTORE = (ushort) ( (_SplusNVRAM.GISTORE + 1) ) ; 
        __context__.SourceCodeLine = 145;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.GISTORE > 20 ))  ) ) 
            { 
            __context__.SourceCodeLine = 147;
            _SplusNVRAM.GISTORE = (ushort) ( 1 ) ; 
            } 
        
        __context__.SourceCodeLine = 150;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.GISTORE != _SplusNVRAM.GINEXT))  ) ) 
            { 
            __context__.SourceCodeLine = 152;
            _SplusNVRAM.GITOSEND = (ushort) ( 1 ) ; 
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object GAMMA_VALUE_OnChange_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        CrestronString INCMD;
        INCMD  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
        
        
        __context__.SourceCodeLine = 160;
        INCMD  .UpdateValue ( ": " + ADDRESS + " " + "GABS" + Functions.ItoA (  (int) ( GAMMA_VALUE  .UshortValue ) ) + "\r"  ) ; 
        __context__.SourceCodeLine = 162;
        _SplusNVRAM.CMD_QUE [ _SplusNVRAM.GISTORE ]  .UpdateValue ( INCMD  ) ; 
        __context__.SourceCodeLine = 164;
        _SplusNVRAM.GISTORE = (ushort) ( (_SplusNVRAM.GISTORE + 1) ) ; 
        __context__.SourceCodeLine = 165;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.GISTORE > 20 ))  ) ) 
            { 
            __context__.SourceCodeLine = 167;
            _SplusNVRAM.GISTORE = (ushort) ( 1 ) ; 
            } 
        
        __context__.SourceCodeLine = 170;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.GISTORE != _SplusNVRAM.GINEXT))  ) ) 
            { 
            __context__.SourceCodeLine = 172;
            _SplusNVRAM.GITOSEND = (ushort) ( 1 ) ; 
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CONTRAST_IN_OnChange_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        CrestronString INCMD;
        INCMD  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
        
        
        __context__.SourceCodeLine = 180;
        INCMD  .UpdateValue ( ": " + ADDRESS + " " + "CNTR " + Functions.ItoA (  (int) ( CONTRAST_IN  .UshortValue ) ) + "\r"  ) ; 
        __context__.SourceCodeLine = 182;
        TO_DEVICE__DOLLAR__  .UpdateValue ( INCMD  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object BRIGHTNESS_IN_OnChange_5 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        CrestronString INCMD;
        INCMD  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
        
        
        __context__.SourceCodeLine = 190;
        INCMD  .UpdateValue ( ": " + ADDRESS + " " + "BRIG " + Functions.ItoA (  (int) ( BRIGHTNESS_IN  .UshortValue ) ) + "\r"  ) ; 
        __context__.SourceCodeLine = 192;
        TO_DEVICE__DOLLAR__  .UpdateValue ( INCMD  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SATURATION_IN_OnChange_6 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        CrestronString INCMD;
        INCMD  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
        
        
        __context__.SourceCodeLine = 200;
        INCMD  .UpdateValue ( ": " + ADDRESS + " " + "CSAT " + Functions.ItoA (  (int) ( SATURATION_IN  .UshortValue ) ) + "\r"  ) ; 
        __context__.SourceCodeLine = 202;
        TO_DEVICE__DOLLAR__  .UpdateValue ( INCMD  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object QUEUE__DOLLAR___OnChange_7 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 211;
        while ( Functions.TestForTrue  ( ( Functions.Find( "\u000D" , QUEUE__DOLLAR__ ))  ) ) 
            { 
            __context__.SourceCodeLine = 214;
            _SplusNVRAM.CMD_QUE [ _SplusNVRAM.GISTORE ]  .UpdateValue ( Functions.Remove ( "\u000D" , QUEUE__DOLLAR__ )  ) ; 
            __context__.SourceCodeLine = 216;
            _SplusNVRAM.GISTORE = (ushort) ( (_SplusNVRAM.GISTORE + 1) ) ; 
            __context__.SourceCodeLine = 217;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.GISTORE > 20 ))  ) ) 
                { 
                __context__.SourceCodeLine = 219;
                _SplusNVRAM.GISTORE = (ushort) ( 1 ) ; 
                } 
            
            __context__.SourceCodeLine = 222;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.GISTORE != _SplusNVRAM.GINEXT))  ) ) 
                { 
                __context__.SourceCodeLine = 224;
                _SplusNVRAM.GITOSEND = (ushort) ( 1 ) ; 
                } 
            
            __context__.SourceCodeLine = 211;
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object FROM_DEVICE__DOLLAR___OnChange_8 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        CrestronString STEMPIN;
        STEMPIN  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 200, this );
        
        CrestronString STEMPVALUE;
        STEMPVALUE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
        
        ushort ILOC1 = 0;
        ushort ILOC2 = 0;
        
        
        __context__.SourceCodeLine = 237;
        while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "\u000D" , FROM_DEVICE__DOLLAR__ ) > 2 ))  ) ) 
            { 
            __context__.SourceCodeLine = 240;
            STEMPIN  .UpdateValue ( Functions.Remove ( "\u000D" , FROM_DEVICE__DOLLAR__ )  ) ; 
            __context__.SourceCodeLine = 242;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (Functions.Find( "!" , STEMPIN ) == 0) ) && Functions.TestForTrue ( Functions.BoolToInt ( Functions.Find( Functions.Chr( (int)( 37 ) ) , STEMPIN ) > 0 ) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 244;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( Functions.Find( "POWR" , STEMPIN ) > 0 ) ) || Functions.TestForTrue ( Functions.BoolToInt ( Functions.Find( "POST" , STEMPIN ) > 0 ) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 246;
                    ILOC1 = (ushort) ( (Functions.Find( "P" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 247;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 248;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 249;
                    POWER_STATUS  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 251;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "IABS" , STEMPIN ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 253;
                    ILOC1 = (ushort) ( (Functions.Find( "IABS" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 254;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 255;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 256;
                    INPUT_STATUS  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 258;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "PMUT" , STEMPIN ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 260;
                    ILOC1 = (ushort) ( (Functions.Find( "PMUT" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 261;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 262;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 263;
                    PICTUREMUTE_STATUS  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 265;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "FRZE" , STEMPIN ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 267;
                    ILOC1 = (ushort) ( (Functions.Find( "FRZE" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 268;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 269;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 270;
                    FREEZE_STATUS  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 272;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "SABS" , STEMPIN ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 274;
                    ILOC1 = (ushort) ( (Functions.Find( "SABS" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 275;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 276;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 277;
                    ASPECT_STATUS  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 279;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "CSAT" , STEMPIN ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 281;
                    ILOC1 = (ushort) ( (Functions.Find( "CSAT" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 282;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 283;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 284;
                    SATURATION_LEVEL  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 286;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "CNTR" , STEMPIN ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 289;
                    ILOC1 = (ushort) ( (Functions.Find( "CNTR" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 290;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 291;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 292;
                    CONTRAST_LEVEL  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 295;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "BRIG" , STEMPIN ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 297;
                    ILOC1 = (ushort) ( (Functions.Find( "BRIG" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 298;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 299;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 300;
                    BRIGHTNESS_LEVEL  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 302;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "GABS" , STEMPIN ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 304;
                    ILOC1 = (ushort) ( (Functions.Find( "GABS" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 305;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 306;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 307;
                    GAMMA_STATUS  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 309;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "LTR1" , STEMPIN ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 311;
                    ILOC1 = (ushort) ( (Functions.Find( "LTR1" , STEMPIN ) + 5) ) ; 
                    __context__.SourceCodeLine = 312;
                    ILOC2 = (ushort) ( Functions.Find( "\u000D" , STEMPIN , ILOC1 ) ) ; 
                    __context__.SourceCodeLine = 313;
                    STEMPVALUE  .UpdateValue ( Functions.Mid ( STEMPIN ,  (int) ( ILOC1 ) ,  (int) ( (ILOC2 - ILOC1) ) )  ) ; 
                    __context__.SourceCodeLine = 314;
                    LAMP_HOURS  .Value = (ushort) ( Functions.Atoi( STEMPVALUE ) ) ; 
                    } 
                
                } 
            
            __context__.SourceCodeLine = 237;
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 328;
        _SplusNVRAM.GITOSEND = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 329;
        _SplusNVRAM.GISTORE = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 330;
        _SplusNVRAM.GINEXT = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 331;
        _SplusNVRAM.GIPOLLNEXT = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 332;
        _SplusNVRAM.GIPOLL_PICTURE = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 336;
        _SplusNVRAM.GSPOLLCMD [ 1 ]  .UpdateValue ( "POST?"  ) ; 
        __context__.SourceCodeLine = 337;
        _SplusNVRAM.GSPOLLCMD [ 2 ]  .UpdateValue ( "IABS?"  ) ; 
        __context__.SourceCodeLine = 338;
        _SplusNVRAM.GSPOLLCMD [ 3 ]  .UpdateValue ( "PMUT?"  ) ; 
        __context__.SourceCodeLine = 339;
        _SplusNVRAM.GSPOLLCMD [ 4 ]  .UpdateValue ( "FRZE?"  ) ; 
        __context__.SourceCodeLine = 340;
        _SplusNVRAM.GSPOLLCMD [ 5 ]  .UpdateValue ( "SABS?"  ) ; 
        __context__.SourceCodeLine = 343;
        _SplusNVRAM.GSPOLL_PICTURE [ 1 ]  .UpdateValue ( "CNTR?"  ) ; 
        __context__.SourceCodeLine = 344;
        _SplusNVRAM.GSPOLL_PICTURE [ 2 ]  .UpdateValue ( "BRIG?"  ) ; 
        __context__.SourceCodeLine = 345;
        _SplusNVRAM.GSPOLL_PICTURE [ 3 ]  .UpdateValue ( "CSAT?"  ) ; 
        __context__.SourceCodeLine = 346;
        _SplusNVRAM.GSPOLL_PICTURE [ 4 ]  .UpdateValue ( "GABS?"  ) ; 
        __context__.SourceCodeLine = 348;
        WaitForInitializationComplete ( ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    _SplusNVRAM.CMD_QUE  = new CrestronString[ 151 ];
    for( uint i = 0; i < 151; i++ )
        _SplusNVRAM.CMD_QUE [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
    _SplusNVRAM.GSPOLLCMD  = new CrestronString[ 6 ];
    for( uint i = 0; i < 6; i++ )
        _SplusNVRAM.GSPOLLCMD [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
    _SplusNVRAM.GSPOLL_PICTURE  = new CrestronString[ 5 ];
    for( uint i = 0; i < 5; i++ )
        _SplusNVRAM.GSPOLL_PICTURE [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
    
    POLL = new Crestron.Logos.SplusObjects.DigitalInput( POLL__DigitalInput__, this );
    m_DigitalInputList.Add( POLL__DigitalInput__, POLL );
    
    POLL_PICTURE = new Crestron.Logos.SplusObjects.DigitalInput( POLL_PICTURE__DigitalInput__, this );
    m_DigitalInputList.Add( POLL_PICTURE__DigitalInput__, POLL_PICTURE );
    
    INPUT_VALUE = new Crestron.Logos.SplusObjects.AnalogInput( INPUT_VALUE__AnalogSerialInput__, this );
    m_AnalogInputList.Add( INPUT_VALUE__AnalogSerialInput__, INPUT_VALUE );
    
    ASPECT_VALUE = new Crestron.Logos.SplusObjects.AnalogInput( ASPECT_VALUE__AnalogSerialInput__, this );
    m_AnalogInputList.Add( ASPECT_VALUE__AnalogSerialInput__, ASPECT_VALUE );
    
    GAMMA_VALUE = new Crestron.Logos.SplusObjects.AnalogInput( GAMMA_VALUE__AnalogSerialInput__, this );
    m_AnalogInputList.Add( GAMMA_VALUE__AnalogSerialInput__, GAMMA_VALUE );
    
    CONTRAST_IN = new Crestron.Logos.SplusObjects.AnalogInput( CONTRAST_IN__AnalogSerialInput__, this );
    m_AnalogInputList.Add( CONTRAST_IN__AnalogSerialInput__, CONTRAST_IN );
    
    BRIGHTNESS_IN = new Crestron.Logos.SplusObjects.AnalogInput( BRIGHTNESS_IN__AnalogSerialInput__, this );
    m_AnalogInputList.Add( BRIGHTNESS_IN__AnalogSerialInput__, BRIGHTNESS_IN );
    
    SATURATION_IN = new Crestron.Logos.SplusObjects.AnalogInput( SATURATION_IN__AnalogSerialInput__, this );
    m_AnalogInputList.Add( SATURATION_IN__AnalogSerialInput__, SATURATION_IN );
    
    POWER_STATUS = new Crestron.Logos.SplusObjects.AnalogOutput( POWER_STATUS__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( POWER_STATUS__AnalogSerialOutput__, POWER_STATUS );
    
    INPUT_STATUS = new Crestron.Logos.SplusObjects.AnalogOutput( INPUT_STATUS__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( INPUT_STATUS__AnalogSerialOutput__, INPUT_STATUS );
    
    PICTUREMUTE_STATUS = new Crestron.Logos.SplusObjects.AnalogOutput( PICTUREMUTE_STATUS__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( PICTUREMUTE_STATUS__AnalogSerialOutput__, PICTUREMUTE_STATUS );
    
    FREEZE_STATUS = new Crestron.Logos.SplusObjects.AnalogOutput( FREEZE_STATUS__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( FREEZE_STATUS__AnalogSerialOutput__, FREEZE_STATUS );
    
    ASPECT_STATUS = new Crestron.Logos.SplusObjects.AnalogOutput( ASPECT_STATUS__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( ASPECT_STATUS__AnalogSerialOutput__, ASPECT_STATUS );
    
    GAMMA_STATUS = new Crestron.Logos.SplusObjects.AnalogOutput( GAMMA_STATUS__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( GAMMA_STATUS__AnalogSerialOutput__, GAMMA_STATUS );
    
    BRIGHTNESS_LEVEL = new Crestron.Logos.SplusObjects.AnalogOutput( BRIGHTNESS_LEVEL__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( BRIGHTNESS_LEVEL__AnalogSerialOutput__, BRIGHTNESS_LEVEL );
    
    SATURATION_LEVEL = new Crestron.Logos.SplusObjects.AnalogOutput( SATURATION_LEVEL__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( SATURATION_LEVEL__AnalogSerialOutput__, SATURATION_LEVEL );
    
    CONTRAST_LEVEL = new Crestron.Logos.SplusObjects.AnalogOutput( CONTRAST_LEVEL__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( CONTRAST_LEVEL__AnalogSerialOutput__, CONTRAST_LEVEL );
    
    LAMP_HOURS = new Crestron.Logos.SplusObjects.AnalogOutput( LAMP_HOURS__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( LAMP_HOURS__AnalogSerialOutput__, LAMP_HOURS );
    
    TO_DEVICE__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( TO_DEVICE__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( TO_DEVICE__DOLLAR____AnalogSerialOutput__, TO_DEVICE__DOLLAR__ );
    
    QUEUE__DOLLAR__ = new Crestron.Logos.SplusObjects.BufferInput( QUEUE__DOLLAR____AnalogSerialInput__, 150, this );
    m_StringInputList.Add( QUEUE__DOLLAR____AnalogSerialInput__, QUEUE__DOLLAR__ );
    
    FROM_DEVICE__DOLLAR__ = new Crestron.Logos.SplusObjects.BufferInput( FROM_DEVICE__DOLLAR____AnalogSerialInput__, 150, this );
    m_StringInputList.Add( FROM_DEVICE__DOLLAR____AnalogSerialInput__, FROM_DEVICE__DOLLAR__ );
    
    ADDRESS = new StringParameter( ADDRESS__Parameter__, this );
    m_ParameterList.Add( ADDRESS__Parameter__, ADDRESS );
    
    
    POLL.OnDigitalPush.Add( new InputChangeHandlerWrapper( POLL_OnPush_0, false ) );
    INPUT_VALUE.OnAnalogChange.Add( new InputChangeHandlerWrapper( INPUT_VALUE_OnChange_1, false ) );
    ASPECT_VALUE.OnAnalogChange.Add( new InputChangeHandlerWrapper( ASPECT_VALUE_OnChange_2, false ) );
    GAMMA_VALUE.OnAnalogChange.Add( new InputChangeHandlerWrapper( GAMMA_VALUE_OnChange_3, false ) );
    CONTRAST_IN.OnAnalogChange.Add( new InputChangeHandlerWrapper( CONTRAST_IN_OnChange_4, false ) );
    BRIGHTNESS_IN.OnAnalogChange.Add( new InputChangeHandlerWrapper( BRIGHTNESS_IN_OnChange_5, false ) );
    SATURATION_IN.OnAnalogChange.Add( new InputChangeHandlerWrapper( SATURATION_IN_OnChange_6, false ) );
    QUEUE__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( QUEUE__DOLLAR___OnChange_7, false ) );
    FROM_DEVICE__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( FROM_DEVICE__DOLLAR___OnChange_8, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_BARCO_PROJECTOR_F_SERIES_PROCESSOR ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint POLL__DigitalInput__ = 0;
const uint POLL_PICTURE__DigitalInput__ = 1;
const uint INPUT_VALUE__AnalogSerialInput__ = 0;
const uint ASPECT_VALUE__AnalogSerialInput__ = 1;
const uint GAMMA_VALUE__AnalogSerialInput__ = 2;
const uint CONTRAST_IN__AnalogSerialInput__ = 3;
const uint BRIGHTNESS_IN__AnalogSerialInput__ = 4;
const uint SATURATION_IN__AnalogSerialInput__ = 5;
const uint QUEUE__DOLLAR____AnalogSerialInput__ = 6;
const uint FROM_DEVICE__DOLLAR____AnalogSerialInput__ = 7;
const uint POWER_STATUS__AnalogSerialOutput__ = 0;
const uint INPUT_STATUS__AnalogSerialOutput__ = 1;
const uint PICTUREMUTE_STATUS__AnalogSerialOutput__ = 2;
const uint FREEZE_STATUS__AnalogSerialOutput__ = 3;
const uint ASPECT_STATUS__AnalogSerialOutput__ = 4;
const uint GAMMA_STATUS__AnalogSerialOutput__ = 5;
const uint BRIGHTNESS_LEVEL__AnalogSerialOutput__ = 6;
const uint SATURATION_LEVEL__AnalogSerialOutput__ = 7;
const uint CONTRAST_LEVEL__AnalogSerialOutput__ = 8;
const uint LAMP_HOURS__AnalogSerialOutput__ = 9;
const uint TO_DEVICE__DOLLAR____AnalogSerialOutput__ = 10;
const uint ADDRESS__Parameter__ = 10;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    [SplusStructAttribute(0, false, true)]
            public ushort GINEXT = 0;
            [SplusStructAttribute(1, false, true)]
            public ushort GISTORE = 0;
            [SplusStructAttribute(2, false, true)]
            public ushort GITOSEND = 0;
            [SplusStructAttribute(3, false, true)]
            public ushort GIPOLLNEXT = 0;
            [SplusStructAttribute(4, false, true)]
            public ushort GIPOLL_PICTURE = 0;
            [SplusStructAttribute(5, false, true)]
            public ushort GICNTR_HOLD = 0;
            [SplusStructAttribute(6, false, true)]
            public CrestronString [] CMD_QUE;
            [SplusStructAttribute(7, false, true)]
            public CrestronString [] GSPOLLCMD;
            [SplusStructAttribute(8, false, true)]
            public CrestronString [] GSPOLL_PICTURE;
            
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
