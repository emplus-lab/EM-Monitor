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

namespace UserModule_SNMP_OCTETSTRING_CONTROL_MODULE_V1_1
{
    public class UserModuleClass_SNMP_OCTETSTRING_CONTROL_MODULE_V1_1 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput GET_VALUE;
        Crestron.Logos.SplusObjects.DigitalInput SET_VALUE;
        Crestron.Logos.SplusObjects.StringInput VALUE;
        Crestron.Logos.SplusObjects.BufferInput FROM_PROCESSOR;
        Crestron.Logos.SplusObjects.DigitalOutput IS_INITIALIZED;
        Crestron.Logos.SplusObjects.StringOutput VALUE_FB;
        Crestron.Logos.SplusObjects.StringOutput TO_PROCESSOR;
        StringParameter SNMP_OID;
        ushort MYID = 0;
        ushort PARSINGMODULEBUSY = 0;
        CrestronString PARSEDMODULEMSG;
        ushort RESPONSEMODULEMSGID = 0;
        CrestronString PARSEDMODULETYPE;
        CrestronString PARSEDMODULEOID;
        CrestronString PARSEDMODULEVALUE;
        CrestronString TRASH;
        private CrestronString GETBOUNDSTRING (  SplusExecutionContext __context__, CrestronString SOURCE , CrestronString STARTSTRING , CrestronString ENDSTRING ) 
            { 
            ushort STARTINDEX = 0;
            
            ushort ENDINDEX = 0;
            
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 150, this );
            
            
            __context__.SourceCodeLine = 111;
            RESPONSE  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 113;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( SOURCE ) > 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 115;
                STARTINDEX = (ushort) ( Functions.Find( STARTSTRING , SOURCE ) ) ; 
                __context__.SourceCodeLine = 116;
                ENDINDEX = (ushort) ( Functions.Find( ENDSTRING , SOURCE , (STARTINDEX + 1) ) ) ; 
                __context__.SourceCodeLine = 118;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( STARTINDEX > 0 ) ) && Functions.TestForTrue ( Functions.BoolToInt ( STARTINDEX < ENDINDEX ) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 120;
                    STARTINDEX = (ushort) ( (STARTINDEX + Functions.Length( STARTSTRING )) ) ; 
                    __context__.SourceCodeLine = 122;
                    RESPONSE  .UpdateValue ( Functions.Mid ( SOURCE ,  (int) ( STARTINDEX ) ,  (int) ( (ENDINDEX - STARTINDEX) ) )  ) ; 
                    } 
                
                } 
            
            __context__.SourceCodeLine = 126;
            return ( RESPONSE ) ; 
            
            }
            
        private void GETINITIALIZED (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 134;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( MYID > 0 ))  ) ) 
                {
                __context__.SourceCodeLine = 135;
                MakeString ( TO_PROCESSOR , "GET<{0:d}|{1}>", (short)MYID, SNMP_OID ) ; 
                }
            
            
            }
            
        private void UPDATEMODULEFEEDBACK (  SplusExecutionContext __context__, CrestronString VALUE ) 
            { 
            
            __context__.SourceCodeLine = 140;
            VALUE_FB  .UpdateValue ( VALUE  ) ; 
            
            }
            
        private ushort STARTSWITH (  SplusExecutionContext __context__, CrestronString MATCH_STRING , CrestronString SOURCE_STRING ) 
            { 
            
            __context__.SourceCodeLine = 148;
            return (ushort)( Functions.BoolToInt (Functions.Find( MATCH_STRING , SOURCE_STRING ) == 1)) ; 
            
            }
            
        private ushort CONTAINS (  SplusExecutionContext __context__, CrestronString MATCH_STRING , CrestronString SOURCE_STRING ) 
            { 
            
            __context__.SourceCodeLine = 153;
            return (ushort)( Functions.BoolToInt ( Functions.Find( MATCH_STRING , SOURCE_STRING ) > 0 )) ; 
            
            }
            
        private uint ATOL_SIGNED (  SplusExecutionContext __context__, CrestronString VALUE ) 
            { 
            
            __context__.SourceCodeLine = 158;
            if ( Functions.TestForTrue  ( ( STARTSWITH( __context__ , "-" , VALUE ))  ) ) 
                {
                __context__.SourceCodeLine = 159;
                return (uint)( (0 - Functions.Atol( VALUE ))) ; 
                }
            
            else 
                {
                __context__.SourceCodeLine = 161;
                return (uint)( Functions.Atol( VALUE )) ; 
                }
            
            
            return 0; // default return value (none specified in module)
            }
            
        object GET_VALUE_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 170;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( MYID > 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 171;
                    MakeString ( TO_PROCESSOR , "GET<{0:d}|{1}>", (short)MYID, SNMP_OID ) ; 
                    }
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object SET_VALUE_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 177;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( MYID > 0 ))  ) ) 
                {
                __context__.SourceCodeLine = 178;
                MakeString ( TO_PROCESSOR , "SET<{0:d}|STRING|{1}|{2}>", (short)MYID, SNMP_OID , VALUE ) ; 
                }
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object FROM_PROCESSOR_OnChange_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 183;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PARSINGMODULEBUSY == 0))  ) ) 
            { 
            __context__.SourceCodeLine = 185;
            PARSINGMODULEBUSY = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 187;
            while ( Functions.TestForTrue  ( ( 1)  ) ) 
                { 
                __context__.SourceCodeLine = 189;
                PARSEDMODULEMSG  .UpdateValue ( Functions.Gather ( ">" , FROM_PROCESSOR )  ) ; 
                __context__.SourceCodeLine = 191;
                if ( Functions.TestForTrue  ( ( CONTAINS( __context__ , PARSEDMODULEMSG , FROM_PROCESSOR ))  ) ) 
                    {
                    __context__.SourceCodeLine = 192;
                    Functions.ClearBuffer ( FROM_PROCESSOR ) ; 
                    }
                
                __context__.SourceCodeLine = 194;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( PARSEDMODULEMSG ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 197;
                    if ( Functions.TestForTrue  ( ( CONTAINS( __context__ , "REGISTER" , PARSEDMODULEMSG ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 199;
                        MYID = (ushort) ( Functions.Atoi( GETBOUNDSTRING( __context__ , PARSEDMODULEMSG , "<" , ">" ) ) ) ; 
                        __context__.SourceCodeLine = 201;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( MYID > 0 ))  ) ) 
                            {
                            __context__.SourceCodeLine = 202;
                            MakeString ( TO_PROCESSOR , "REGISTER<{0:d}>", (short)MYID) ; 
                            }
                        
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 205;
                        if ( Functions.TestForTrue  ( ( CONTAINS( __context__ , "INIT" , PARSEDMODULEMSG ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 207;
                            IS_INITIALIZED  .Value = (ushort) ( 0 ) ; 
                            __context__.SourceCodeLine = 208;
                            GETINITIALIZED (  __context__  ) ; 
                            } 
                        
                        else 
                            {
                            __context__.SourceCodeLine = 211;
                            if ( Functions.TestForTrue  ( ( CONTAINS( __context__ , "RESPONSE" , PARSEDMODULEMSG ))  ) ) 
                                { 
                                __context__.SourceCodeLine = 213;
                                RESPONSEMODULEMSGID = (ushort) ( Functions.Atoi( GETBOUNDSTRING( __context__ , PARSEDMODULEMSG , "<" , "|" ) ) ) ; 
                                __context__.SourceCodeLine = 215;
                                PARSEDMODULEOID  .UpdateValue ( GETBOUNDSTRING (  __context__ , PARSEDMODULEMSG, "|", "|")  ) ; 
                                __context__.SourceCodeLine = 216;
                                TRASH  .UpdateValue ( Functions.Remove ( "|" , PARSEDMODULEMSG )  ) ; 
                                __context__.SourceCodeLine = 218;
                                PARSEDMODULEVALUE  .UpdateValue ( GETBOUNDSTRING (  __context__ , PARSEDMODULEMSG, "|", ">")  ) ; 
                                __context__.SourceCodeLine = 220;
                                UPDATEMODULEFEEDBACK (  __context__ , PARSEDMODULEVALUE) ; 
                                __context__.SourceCodeLine = 222;
                                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( MYID > 0 ))  ) ) 
                                    {
                                    __context__.SourceCodeLine = 223;
                                    MakeString ( TO_PROCESSOR , "RESPONSE_OK<{0:d}>", (short)MYID) ; 
                                    }
                                
                                __context__.SourceCodeLine = 225;
                                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (IS_INITIALIZED  .Value == 0))  ) ) 
                                    { 
                                    __context__.SourceCodeLine = 227;
                                    IS_INITIALIZED  .Value = (ushort) ( 1 ) ; 
                                    __context__.SourceCodeLine = 229;
                                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( MYID > 0 ))  ) ) 
                                        {
                                        __context__.SourceCodeLine = 230;
                                        MakeString ( TO_PROCESSOR , "INIT_DONE<{0:d}>", (short)MYID) ; 
                                        }
                                    
                                    } 
                                
                                } 
                            
                            }
                        
                        }
                    
                    } 
                
                __context__.SourceCodeLine = 187;
                } 
            
            __context__.SourceCodeLine = 236;
            PARSINGMODULEBUSY = (ushort) ( 0 ) ; 
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
        
        __context__.SourceCodeLine = 248;
        PARSINGMODULEBUSY = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 250;
        IS_INITIALIZED  .Value = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 252;
        MYID = (ushort) ( 0 ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    PARSEDMODULEMSG  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 254, this );
    PARSEDMODULETYPE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 150, this );
    PARSEDMODULEOID  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 150, this );
    PARSEDMODULEVALUE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 128, this );
    TRASH  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 150, this );
    
    GET_VALUE = new Crestron.Logos.SplusObjects.DigitalInput( GET_VALUE__DigitalInput__, this );
    m_DigitalInputList.Add( GET_VALUE__DigitalInput__, GET_VALUE );
    
    SET_VALUE = new Crestron.Logos.SplusObjects.DigitalInput( SET_VALUE__DigitalInput__, this );
    m_DigitalInputList.Add( SET_VALUE__DigitalInput__, SET_VALUE );
    
    IS_INITIALIZED = new Crestron.Logos.SplusObjects.DigitalOutput( IS_INITIALIZED__DigitalOutput__, this );
    m_DigitalOutputList.Add( IS_INITIALIZED__DigitalOutput__, IS_INITIALIZED );
    
    VALUE = new Crestron.Logos.SplusObjects.StringInput( VALUE__AnalogSerialInput__, 128, this );
    m_StringInputList.Add( VALUE__AnalogSerialInput__, VALUE );
    
    VALUE_FB = new Crestron.Logos.SplusObjects.StringOutput( VALUE_FB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( VALUE_FB__AnalogSerialOutput__, VALUE_FB );
    
    TO_PROCESSOR = new Crestron.Logos.SplusObjects.StringOutput( TO_PROCESSOR__AnalogSerialOutput__, this );
    m_StringOutputList.Add( TO_PROCESSOR__AnalogSerialOutput__, TO_PROCESSOR );
    
    FROM_PROCESSOR = new Crestron.Logos.SplusObjects.BufferInput( FROM_PROCESSOR__AnalogSerialInput__, 1024, this );
    m_StringInputList.Add( FROM_PROCESSOR__AnalogSerialInput__, FROM_PROCESSOR );
    
    SNMP_OID = new StringParameter( SNMP_OID__Parameter__, this );
    m_ParameterList.Add( SNMP_OID__Parameter__, SNMP_OID );
    
    
    GET_VALUE.OnDigitalPush.Add( new InputChangeHandlerWrapper( GET_VALUE_OnPush_0, false ) );
    SET_VALUE.OnDigitalPush.Add( new InputChangeHandlerWrapper( SET_VALUE_OnPush_1, false ) );
    FROM_PROCESSOR.OnSerialChange.Add( new InputChangeHandlerWrapper( FROM_PROCESSOR_OnChange_2, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SNMP_OCTETSTRING_CONTROL_MODULE_V1_1 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint GET_VALUE__DigitalInput__ = 0;
const uint SET_VALUE__DigitalInput__ = 1;
const uint VALUE__AnalogSerialInput__ = 0;
const uint FROM_PROCESSOR__AnalogSerialInput__ = 1;
const uint IS_INITIALIZED__DigitalOutput__ = 0;
const uint VALUE_FB__AnalogSerialOutput__ = 0;
const uint TO_PROCESSOR__AnalogSerialOutput__ = 1;
const uint SNMP_OID__Parameter__ = 10;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
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
