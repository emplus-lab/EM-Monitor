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

namespace UserModule_SNMP_PDU_STRING_DECODE
{
    public class UserModuleClass_SNMP_PDU_STRING_DECODE : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        Crestron.Logos.SplusObjects.StringOutput PDU_STR_OUTPUT;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> IN;
        Crestron.Logos.SplusObjects.StringInput PDU_STR_INPUT;
        Crestron.Logos.SplusObjects.DigitalOutput SNMP_WRITE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> OUT;
        object PDU_STR_INPUT_OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                ushort I = 0;
                
                
                __context__.SourceCodeLine = 186;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 0 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)7; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 188;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( PDU_STR_INPUT , (int)( (I + (I + 1)) ) , (int)( 1 ) ) == "1"))  ) ) 
                        {
                        __context__.SourceCodeLine = 189;
                        OUT [ (I + 1)]  .Value = (ushort) ( 1 ) ; 
                        }
                    
                    else 
                        {
                        __context__.SourceCodeLine = 191;
                        OUT [ (I + 1)]  .Value = (ushort) ( 0 ) ; 
                        }
                    
                    __context__.SourceCodeLine = 186;
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object IN_OnChange_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            ushort I = 0;
            
            CrestronString SNMP_MESS;
            SNMP_MESS  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 16, this );
            
            
            __context__.SourceCodeLine = 205;
            PDU_STR_OUTPUT  .UpdateValue ( Functions.ItoA (  (int) ( IN[ 1 ] .Value ) ) + "," + Functions.ItoA (  (int) ( IN[ 2 ] .Value ) ) + "," + Functions.ItoA (  (int) ( IN[ 3 ] .Value ) ) + "," + Functions.ItoA (  (int) ( IN[ 4 ] .Value ) ) + "," + Functions.ItoA (  (int) ( IN[ 5 ] .Value ) ) + "," + Functions.ItoA (  (int) ( IN[ 6 ] .Value ) ) + "," + Functions.ItoA (  (int) ( IN[ 7 ] .Value ) ) + "," + Functions.ItoA (  (int) ( IN[ 8 ] .Value ) )  ) ; 
            __context__.SourceCodeLine = 215;
            Functions.Pulse ( 10, SNMP_WRITE ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    
    IN = new InOutArray<DigitalInput>( 8, this );
    for( uint i = 0; i < 8; i++ )
    {
        IN[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( IN__DigitalInput__ + i, IN__DigitalInput__, this );
        m_DigitalInputList.Add( IN__DigitalInput__ + i, IN[i+1] );
    }
    
    SNMP_WRITE = new Crestron.Logos.SplusObjects.DigitalOutput( SNMP_WRITE__DigitalOutput__, this );
    m_DigitalOutputList.Add( SNMP_WRITE__DigitalOutput__, SNMP_WRITE );
    
    OUT = new InOutArray<DigitalOutput>( 8, this );
    for( uint i = 0; i < 8; i++ )
    {
        OUT[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( OUT__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( OUT__DigitalOutput__ + i, OUT[i+1] );
    }
    
    PDU_STR_INPUT = new Crestron.Logos.SplusObjects.StringInput( PDU_STR_INPUT__AnalogSerialInput__, 16, this );
    m_StringInputList.Add( PDU_STR_INPUT__AnalogSerialInput__, PDU_STR_INPUT );
    
    PDU_STR_OUTPUT = new Crestron.Logos.SplusObjects.StringOutput( PDU_STR_OUTPUT__AnalogSerialOutput__, this );
    m_StringOutputList.Add( PDU_STR_OUTPUT__AnalogSerialOutput__, PDU_STR_OUTPUT );
    
    
    PDU_STR_INPUT.OnSerialChange.Add( new InputChangeHandlerWrapper( PDU_STR_INPUT_OnChange_0, false ) );
    for( uint i = 0; i < 8; i++ )
        IN[i+1].OnDigitalChange.Add( new InputChangeHandlerWrapper( IN_OnChange_1, false ) );
        
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SNMP_PDU_STRING_DECODE ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint PDU_STR_OUTPUT__AnalogSerialOutput__ = 0;
const uint IN__DigitalInput__ = 0;
const uint PDU_STR_INPUT__AnalogSerialInput__ = 0;
const uint SNMP_WRITE__DigitalOutput__ = 0;
const uint OUT__DigitalOutput__ = 1;

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
