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

namespace UserModule_SONY_GTZ280
{
    public class UserModuleClass_SONY_GTZ280 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        Crestron.Logos.SplusObjects.DigitalInput POWER_ON;
        Crestron.Logos.SplusObjects.DigitalInput POWER_OFF;
        Crestron.Logos.SplusObjects.DigitalInput ACTIVATE_3D;
        Crestron.Logos.SplusObjects.DigitalInput DEACTIVATE_3D;
        Crestron.Logos.SplusObjects.DigitalInput SELECT_INPUTS_1234;
        Crestron.Logos.SplusObjects.StringInput LAN_RX;
        Crestron.Logos.SplusObjects.DigitalOutput STATUS_POWER;
        Crestron.Logos.SplusObjects.DigitalOutput STATUS_3D;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> STATUS_LAMP_HOURS;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> LAN_TX;
        object POWER_ON_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 164;
                MakeString ( LAN_TX [ 0] , "Power \"on\"\r\n") ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object POWER_OFF_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 169;
            MakeString ( LAN_TX [ 0] , "Power \"off\"\r\n") ; 
            
            
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
    
    POWER_ON = new Crestron.Logos.SplusObjects.DigitalInput( POWER_ON__DigitalInput__, this );
    m_DigitalInputList.Add( POWER_ON__DigitalInput__, POWER_ON );
    
    POWER_OFF = new Crestron.Logos.SplusObjects.DigitalInput( POWER_OFF__DigitalInput__, this );
    m_DigitalInputList.Add( POWER_OFF__DigitalInput__, POWER_OFF );
    
    ACTIVATE_3D = new Crestron.Logos.SplusObjects.DigitalInput( ACTIVATE_3D__DigitalInput__, this );
    m_DigitalInputList.Add( ACTIVATE_3D__DigitalInput__, ACTIVATE_3D );
    
    DEACTIVATE_3D = new Crestron.Logos.SplusObjects.DigitalInput( DEACTIVATE_3D__DigitalInput__, this );
    m_DigitalInputList.Add( DEACTIVATE_3D__DigitalInput__, DEACTIVATE_3D );
    
    SELECT_INPUTS_1234 = new Crestron.Logos.SplusObjects.DigitalInput( SELECT_INPUTS_1234__DigitalInput__, this );
    m_DigitalInputList.Add( SELECT_INPUTS_1234__DigitalInput__, SELECT_INPUTS_1234 );
    
    STATUS_POWER = new Crestron.Logos.SplusObjects.DigitalOutput( STATUS_POWER__DigitalOutput__, this );
    m_DigitalOutputList.Add( STATUS_POWER__DigitalOutput__, STATUS_POWER );
    
    STATUS_3D = new Crestron.Logos.SplusObjects.DigitalOutput( STATUS_3D__DigitalOutput__, this );
    m_DigitalOutputList.Add( STATUS_3D__DigitalOutput__, STATUS_3D );
    
    LAN_RX = new Crestron.Logos.SplusObjects.StringInput( LAN_RX__AnalogSerialInput__, 256, this );
    m_StringInputList.Add( LAN_RX__AnalogSerialInput__, LAN_RX );
    
    STATUS_LAMP_HOURS = new InOutArray<StringOutput>( 16, this );
    for( uint i = 0; i < 16; i++ )
    {
        STATUS_LAMP_HOURS[i+1] = new Crestron.Logos.SplusObjects.StringOutput( STATUS_LAMP_HOURS__AnalogSerialOutput__ + i, this );
        m_StringOutputList.Add( STATUS_LAMP_HOURS__AnalogSerialOutput__ + i, STATUS_LAMP_HOURS[i+1] );
    }
    
    LAN_TX = new InOutArray<StringOutput>( 256, this );
    for( uint i = 0; i < 256; i++ )
    {
        LAN_TX[i+1] = new Crestron.Logos.SplusObjects.StringOutput( LAN_TX__AnalogSerialOutput__ + i, this );
        m_StringOutputList.Add( LAN_TX__AnalogSerialOutput__ + i, LAN_TX[i+1] );
    }
    
    
    POWER_ON.OnDigitalPush.Add( new InputChangeHandlerWrapper( POWER_ON_OnPush_0, false ) );
    POWER_OFF.OnDigitalPush.Add( new InputChangeHandlerWrapper( POWER_OFF_OnPush_1, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SONY_GTZ280 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint POWER_ON__DigitalInput__ = 0;
const uint POWER_OFF__DigitalInput__ = 1;
const uint ACTIVATE_3D__DigitalInput__ = 2;
const uint DEACTIVATE_3D__DigitalInput__ = 3;
const uint SELECT_INPUTS_1234__DigitalInput__ = 4;
const uint LAN_RX__AnalogSerialInput__ = 0;
const uint STATUS_POWER__DigitalOutput__ = 0;
const uint STATUS_3D__DigitalOutput__ = 1;
const uint STATUS_LAMP_HOURS__AnalogSerialOutput__ = 0;
const uint LAN_TX__AnalogSerialOutput__ = 16;

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
