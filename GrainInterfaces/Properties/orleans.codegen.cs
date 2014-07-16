//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#if !EXCLUDE_CODEGEN
#pragma warning disable 162
#pragma warning disable 219
#pragma warning disable 693
#pragma warning disable 1591
#pragma warning disable 1998

namespace GrainInterfaces
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;
    using System.Collections.Generic;
    using Orleans;
    using System.Collections;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public class AggregatorGrainFactory
    {
        

                        public static IAggregatorGrain GetGrain(long primaryKey)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(IAggregatorGrain), 1374214536, primaryKey));
                        }

                        public static IAggregatorGrain GetGrain(long primaryKey, string grainClassNamePrefix)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(IAggregatorGrain), 1374214536, primaryKey, grainClassNamePrefix));
                        }

                        public static IAggregatorGrain GetGrain(Guid primaryKey)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(IAggregatorGrain), 1374214536, primaryKey));
                        }

                        public static IAggregatorGrain GetGrain(Guid primaryKey, string grainClassNamePrefix)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(IAggregatorGrain), 1374214536, primaryKey, grainClassNamePrefix));
                        }

            public static IAggregatorGrain Cast(IAddressable grainRef)
            {
                
                return AggregatorGrainReference.Cast(grainRef);
            }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
        [System.SerializableAttribute()]
        [Orleans.GrainReferenceAttribute("GrainInterfaces.IAggregatorGrain")]
        internal class AggregatorGrainReference : Orleans.GrainReference, IAggregatorGrain, Orleans.IAddressable
        {
            

            public static IAggregatorGrain Cast(IAddressable grainRef)
            {
                
                return (IAggregatorGrain) GrainReference.CastInternal(typeof(IAggregatorGrain), (GrainReference gr) => { return new AggregatorGrainReference(gr);}, grainRef, 1374214536);
            }

                protected override int InterfaceId { get { return 1374214536; } }

                public override bool IsCompatible(int interfaceId) { return interfaceId == this.InterfaceId; }

                protected override string InterfaceName { get { return "GrainInterfaces.IAggregatorGrain"; } }

                protected override string GetMethodName(int interfaceId, int methodId) { return AggregatorGrainMethodInvoker.GetMethodName(interfaceId, methodId); }
            
            protected internal AggregatorGrainReference(GrainReference reference) : 
                    base(reference)
            {
            }
            
            [Orleans.CopierMethodAttribute()]
            public static object _Copier(object original)
            {
                AggregatorGrainReference input = ((AggregatorGrainReference)(original));
                return ((AggregatorGrainReference)(GrainReference.CopyGrainReference(input)));
            }
            
            [Orleans.SerializerMethodAttribute()]
            public static void _Serializer(object original, Orleans.Serialization.BinaryTokenStreamWriter stream, System.Type expected)
            {
                AggregatorGrainReference input = ((AggregatorGrainReference)(original));
                GrainReference.SerializeGrainReference(input, stream, expected);
            }
            
            [Orleans.DeserializerMethodAttribute()]
            public static object _Deserializer(System.Type expected, Orleans.Serialization.BinaryTokenStreamReader stream)
            {
                return AggregatorGrainReference.Cast(((Orleans.GrainReference)(GrainReference.DeserializeGrainReference(expected, stream))));
            }
            
            public System.Threading.Tasks.Task SetObserver(GrainInterfaces.ISimulationObserver observer)
            {
GrainFactoryBase.CheckGrainObserverParamInternal(observer);

                return base.InvokeMethodAsync<object>(903001954, new object[] {observer is GrainBase ? GrainInterfaces.SimulationObserverFactory.Cast(observer.AsReference()) : observer}, TimeSpan.Zero );
            }
            
            public System.Threading.Tasks.Task AggregateResults(long id, int total_requests, int failed_requests)
            {

                return base.InvokeMethodAsync<object>(-638542766, new object[] {id, total_requests, failed_requests}, TimeSpan.Zero );
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    [Orleans.MethodInvokerAttribute("GrainInterfaces.IAggregatorGrain", 1374214536)]
    internal class AggregatorGrainMethodInvoker : IGrainMethodInvoker
    {
        
        public int InterfaceId
        {
            get
            {
                return 1374214536;
            }
        }
        
        public async System.Threading.Tasks.Task<Object> Invoke(IAddressable grain, int interfaceId, int methodId, object[] arguments)
        {
                if (grain == null) throw new System.ArgumentNullException("grain");
                switch (interfaceId)
                {
                    case 1374214536:  // IAggregatorGrain
                        switch (methodId)
                        {
                            case 903001954: 
                                await ((IAggregatorGrain)grain).SetObserver((ISimulationObserver)arguments[0]);
                              return true;
                            case -638542766: 
                                await ((IAggregatorGrain)grain).AggregateResults((Int64)arguments[0], (Int32)arguments[1], (Int32)arguments[2]);
                              return true;
                            default: 
                                throw new NotImplementedException("interfaceId="+interfaceId+",methodId="+methodId);
                        }
                    default:
                        throw new System.InvalidCastException("interfaceId="+interfaceId);
                }
        }
        
        public static string GetMethodName(int interfaceId, int methodId)
        {

            switch (interfaceId)
            {
                
                case 1374214536:  // IAggregatorGrain
                    switch (methodId)
                    {
                        case 903001954:
                            return "SetObserver";
                    case -638542766:
                            return "AggregateResults";
                    case -606142484:
                            return "GetProperties";
                    
                        default: 
                            throw new NotImplementedException("interfaceId="+interfaceId+",methodId="+methodId);
                    }

                default:
                    throw new System.InvalidCastException("interfaceId="+interfaceId);
            }
        }
    }
    
    [Serializable()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public class AggregatorGrainProperties
    {
        

            public Dictionary<string,object> AsDictionary()
            {  
                var retValue = new Dictionary<string,object>();
                return retValue;
            }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public class SimulatorGrainFactory
    {
        

                        public static ISimulatorGrain GetGrain(long primaryKey)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(ISimulatorGrain), 2113177723, primaryKey));
                        }

                        public static ISimulatorGrain GetGrain(long primaryKey, string grainClassNamePrefix)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(ISimulatorGrain), 2113177723, primaryKey, grainClassNamePrefix));
                        }

                        public static ISimulatorGrain GetGrain(Guid primaryKey)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(ISimulatorGrain), 2113177723, primaryKey));
                        }

                        public static ISimulatorGrain GetGrain(Guid primaryKey, string grainClassNamePrefix)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(ISimulatorGrain), 2113177723, primaryKey, grainClassNamePrefix));
                        }

            public static ISimulatorGrain Cast(IAddressable grainRef)
            {
                
                return SimulatorGrainReference.Cast(grainRef);
            }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
        [System.SerializableAttribute()]
        [Orleans.GrainReferenceAttribute("GrainInterfaces.ISimulatorGrain")]
        internal class SimulatorGrainReference : Orleans.GrainReference, ISimulatorGrain, Orleans.IAddressable
        {
            

            public static ISimulatorGrain Cast(IAddressable grainRef)
            {
                
                return (ISimulatorGrain) GrainReference.CastInternal(typeof(ISimulatorGrain), (GrainReference gr) => { return new SimulatorGrainReference(gr);}, grainRef, 2113177723);
            }

                protected override int InterfaceId { get { return 2113177723; } }

                public override bool IsCompatible(int interfaceId) { return interfaceId == this.InterfaceId; }

                protected override string InterfaceName { get { return "GrainInterfaces.ISimulatorGrain"; } }

                protected override string GetMethodName(int interfaceId, int methodId) { return SimulatorGrainMethodInvoker.GetMethodName(interfaceId, methodId); }
            
            protected internal SimulatorGrainReference(GrainReference reference) : 
                    base(reference)
            {
            }
            
            [Orleans.CopierMethodAttribute()]
            public static object _Copier(object original)
            {
                SimulatorGrainReference input = ((SimulatorGrainReference)(original));
                return ((SimulatorGrainReference)(GrainReference.CopyGrainReference(input)));
            }
            
            [Orleans.SerializerMethodAttribute()]
            public static void _Serializer(object original, Orleans.Serialization.BinaryTokenStreamWriter stream, System.Type expected)
            {
                SimulatorGrainReference input = ((SimulatorGrainReference)(original));
                GrainReference.SerializeGrainReference(input, stream, expected);
            }
            
            [Orleans.DeserializerMethodAttribute()]
            public static object _Deserializer(System.Type expected, Orleans.Serialization.BinaryTokenStreamReader stream)
            {
                return SimulatorGrainReference.Cast(((Orleans.GrainReference)(GrainReference.DeserializeGrainReference(expected, stream))));
            }
            
            public System.Threading.Tasks.Task StartSimulation(long id, string url, GrainInterfaces.IManagerGrain managerGrain)
            {

                return base.InvokeMethodAsync<object>(84807118, new object[] {id, url, managerGrain is GrainBase ? GrainInterfaces.ManagerGrainFactory.Cast(managerGrain.AsReference()) : managerGrain}, TimeSpan.Zero );
            }
            
            public System.Threading.Tasks.Task StopSimulation()
            {

                return base.InvokeMethodAsync<object>(-1598884751, new object[] {}, TimeSpan.Zero );
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    [Orleans.MethodInvokerAttribute("GrainInterfaces.ISimulatorGrain", 2113177723)]
    internal class SimulatorGrainMethodInvoker : IGrainMethodInvoker
    {
        
        public int InterfaceId
        {
            get
            {
                return 2113177723;
            }
        }
        
        public async System.Threading.Tasks.Task<Object> Invoke(IAddressable grain, int interfaceId, int methodId, object[] arguments)
        {
                if (grain == null) throw new System.ArgumentNullException("grain");
                switch (interfaceId)
                {
                    case 2113177723:  // ISimulatorGrain
                        switch (methodId)
                        {
                            case 84807118: 
                                await ((ISimulatorGrain)grain).StartSimulation((Int64)arguments[0], (String)arguments[1], (IManagerGrain)arguments[2]);
                              return true;
                            case -1598884751: 
                                await ((ISimulatorGrain)grain).StopSimulation();
                              return true;
                            default: 
                                throw new NotImplementedException("interfaceId="+interfaceId+",methodId="+methodId);
                        }
                    default:
                        throw new System.InvalidCastException("interfaceId="+interfaceId);
                }
        }
        
        public static string GetMethodName(int interfaceId, int methodId)
        {

            switch (interfaceId)
            {
                
                case 2113177723:  // ISimulatorGrain
                    switch (methodId)
                    {
                        case 84807118:
                            return "StartSimulation";
                    case -1598884751:
                            return "StopSimulation";
                    case -606142484:
                            return "GetProperties";
                    
                        default: 
                            throw new NotImplementedException("interfaceId="+interfaceId+",methodId="+methodId);
                    }

                default:
                    throw new System.InvalidCastException("interfaceId="+interfaceId);
            }
        }
    }
    
    [Serializable()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public class SimulatorGrainProperties
    {
        

            public Dictionary<string,object> AsDictionary()
            {  
                var retValue = new Dictionary<string,object>();
                return retValue;
            }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public class ManagerGrainFactory
    {
        

                        public static IManagerGrain GetGrain(long primaryKey)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(IManagerGrain), -2031832475, primaryKey));
                        }

                        public static IManagerGrain GetGrain(long primaryKey, string grainClassNamePrefix)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(IManagerGrain), -2031832475, primaryKey, grainClassNamePrefix));
                        }

                        public static IManagerGrain GetGrain(Guid primaryKey)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(IManagerGrain), -2031832475, primaryKey));
                        }

                        public static IManagerGrain GetGrain(Guid primaryKey, string grainClassNamePrefix)
                        {
                            return Cast(GrainFactoryBase.MakeGrainReferenceInternal(typeof(IManagerGrain), -2031832475, primaryKey, grainClassNamePrefix));
                        }

            public static IManagerGrain Cast(IAddressable grainRef)
            {
                
                return ManagerGrainReference.Cast(grainRef);
            }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
        [System.SerializableAttribute()]
        [Orleans.GrainReferenceAttribute("GrainInterfaces.IManagerGrain")]
        internal class ManagerGrainReference : Orleans.GrainReference, IManagerGrain, Orleans.IAddressable
        {
            

            public static IManagerGrain Cast(IAddressable grainRef)
            {
                
                return (IManagerGrain) GrainReference.CastInternal(typeof(IManagerGrain), (GrainReference gr) => { return new ManagerGrainReference(gr);}, grainRef, -2031832475);
            }

                protected override int InterfaceId { get { return -2031832475; } }

                public override bool IsCompatible(int interfaceId) { return interfaceId == this.InterfaceId; }

                protected override string InterfaceName { get { return "GrainInterfaces.IManagerGrain"; } }

                protected override string GetMethodName(int interfaceId, int methodId) { return ManagerGrainMethodInvoker.GetMethodName(interfaceId, methodId); }
            
            protected internal ManagerGrainReference(GrainReference reference) : 
                    base(reference)
            {
            }
            
            [Orleans.CopierMethodAttribute()]
            public static object _Copier(object original)
            {
                ManagerGrainReference input = ((ManagerGrainReference)(original));
                return ((ManagerGrainReference)(GrainReference.CopyGrainReference(input)));
            }
            
            [Orleans.SerializerMethodAttribute()]
            public static void _Serializer(object original, Orleans.Serialization.BinaryTokenStreamWriter stream, System.Type expected)
            {
                ManagerGrainReference input = ((ManagerGrainReference)(original));
                GrainReference.SerializeGrainReference(input, stream, expected);
            }
            
            [Orleans.DeserializerMethodAttribute()]
            public static object _Deserializer(System.Type expected, Orleans.Serialization.BinaryTokenStreamReader stream)
            {
                return ManagerGrainReference.Cast(((Orleans.GrainReference)(GrainReference.DeserializeGrainReference(expected, stream))));
            }
            
            public System.Threading.Tasks.Task SetAggregator(GrainInterfaces.IAggregatorGrain aggregator)
            {

                return base.InvokeMethodAsync<object>(1756076699, new object[] {aggregator is GrainBase ? GrainInterfaces.AggregatorGrainFactory.Cast(aggregator.AsReference()) : aggregator}, TimeSpan.Zero );
            }
            
            public System.Threading.Tasks.Task StartSimulators(int delay, int count, string url)
            {

                return base.InvokeMethodAsync<object>(981188829, new object[] {delay, count, url}, TimeSpan.Zero );
            }
            
            public System.Threading.Tasks.Task StopSimulators()
            {

                return base.InvokeMethodAsync<object>(488722972, new object[] {}, TimeSpan.Zero );
            }
            
            public System.Threading.Tasks.Task SendResults(int total_requests, int failed_requests)
            {

                return base.InvokeMethodAsync<object>(-1507404422, new object[] {total_requests, failed_requests}, TimeSpan.Zero );
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    [Orleans.MethodInvokerAttribute("GrainInterfaces.IManagerGrain", -2031832475)]
    internal class ManagerGrainMethodInvoker : IGrainMethodInvoker
    {
        
        public int InterfaceId
        {
            get
            {
                return -2031832475;
            }
        }
        
        public async System.Threading.Tasks.Task<Object> Invoke(IAddressable grain, int interfaceId, int methodId, object[] arguments)
        {
                if (grain == null) throw new System.ArgumentNullException("grain");
                switch (interfaceId)
                {
                    case -2031832475:  // IManagerGrain
                        switch (methodId)
                        {
                            case 1756076699: 
                                await ((IManagerGrain)grain).SetAggregator((IAggregatorGrain)arguments[0]);
                              return true;
                            case 981188829: 
                                await ((IManagerGrain)grain).StartSimulators((Int32)arguments[0], (Int32)arguments[1], (String)arguments[2]);
                              return true;
                            case 488722972: 
                                await ((IManagerGrain)grain).StopSimulators();
                              return true;
                            case -1507404422: 
                                await ((IManagerGrain)grain).SendResults((Int32)arguments[0], (Int32)arguments[1]);
                              return true;
                            default: 
                                throw new NotImplementedException("interfaceId="+interfaceId+",methodId="+methodId);
                        }
                    default:
                        throw new System.InvalidCastException("interfaceId="+interfaceId);
                }
        }
        
        public static string GetMethodName(int interfaceId, int methodId)
        {

            switch (interfaceId)
            {
                
                case -2031832475:  // IManagerGrain
                    switch (methodId)
                    {
                        case 1756076699:
                            return "SetAggregator";
                    case 981188829:
                            return "StartSimulators";
                    case 488722972:
                            return "StopSimulators";
                    case -1507404422:
                            return "SendResults";
                    case -606142484:
                            return "GetProperties";
                    
                        default: 
                            throw new NotImplementedException("interfaceId="+interfaceId+",methodId="+methodId);
                    }

                default:
                    throw new System.InvalidCastException("interfaceId="+interfaceId);
            }
        }
    }
    
    [Serializable()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public class ManagerGrainProperties
    {
        

            public Dictionary<string,object> AsDictionary()
            {  
                var retValue = new Dictionary<string,object>();
                return retValue;
            }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public class SimulationObserverFactory
    {
        

            public static ISimulationObserver Cast(IAddressable grainRef)
            {
                
                return SimulationObserverReference.Cast(grainRef);
            }

        private static IGrainMethodInvoker methodInvoker;

        public async static System.Threading.Tasks.Task<ISimulationObserver> CreateObjectReference(ISimulationObserver obj)
        {
            if (methodInvoker == null) methodInvoker = new SimulationObserverMethodInvoker();
            return SimulationObserverFactory.Cast(await GrainReference.CreateObjectReference(obj, methodInvoker));
        }

        public static void DeleteObjectReference(ISimulationObserver reference)
        {
            GrainReference.DeleteObjectReference(reference);
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
        [System.SerializableAttribute()]
        [Orleans.GrainReferenceAttribute("GrainInterfaces.ISimulationObserver")]
        internal class SimulationObserverReference : Orleans.GrainReference, ISimulationObserver, Orleans.IAddressable
        {
            

            public static ISimulationObserver Cast(IAddressable grainRef)
            {
                
                return (ISimulationObserver) GrainReference.CastInternal(typeof(ISimulationObserver), (GrainReference gr) => { return new SimulationObserverReference(gr);}, grainRef, -2015988020);
            }

                protected override int InterfaceId { get { return -2015988020; } }

                public override bool IsCompatible(int interfaceId) { return interfaceId == this.InterfaceId; }

                protected override string InterfaceName { get { return "GrainInterfaces.ISimulationObserver"; } }

                protected override string GetMethodName(int interfaceId, int methodId) { return SimulationObserverMethodInvoker.GetMethodName(interfaceId, methodId); }
            
            protected internal SimulationObserverReference(GrainReference reference) : 
                    base(reference)
            {
            }
            
            [Orleans.CopierMethodAttribute()]
            public static object _Copier(object original)
            {
                SimulationObserverReference input = ((SimulationObserverReference)(original));
                return ((SimulationObserverReference)(GrainReference.CopyGrainReference(input)));
            }
            
            [Orleans.SerializerMethodAttribute()]
            public static void _Serializer(object original, Orleans.Serialization.BinaryTokenStreamWriter stream, System.Type expected)
            {
                SimulationObserverReference input = ((SimulationObserverReference)(original));
                GrainReference.SerializeGrainReference(input, stream, expected);
            }
            
            [Orleans.DeserializerMethodAttribute()]
            public static object _Deserializer(System.Type expected, Orleans.Serialization.BinaryTokenStreamReader stream)
            {
                return SimulationObserverReference.Cast(((Orleans.GrainReference)(GrainReference.DeserializeGrainReference(expected, stream))));
            }
            
            public void ReportResults(long millis, int sent, int errors, System.Collections.Generic.Dictionary<System.Int64,System.Int32> all_sent, System.Collections.Generic.Dictionary<System.Int64,System.Int32> all_errors)
            {

                base.InvokeOneWayMethod(974898854, new object[] {millis, sent, errors, all_sent, all_errors} );
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    [Orleans.MethodInvokerAttribute("GrainInterfaces.ISimulationObserver", -2015988020)]
    internal class SimulationObserverMethodInvoker : IGrainMethodInvoker
    {
        
        public int InterfaceId
        {
            get
            {
                return -2015988020;
            }
        }
        
        public async System.Threading.Tasks.Task<Object> Invoke(IAddressable grain, int interfaceId, int methodId, object[] arguments)
        {
                if (grain == null) throw new System.ArgumentNullException("grain");
                switch (interfaceId)
                {
                    case -2015988020:  // ISimulationObserver
                        switch (methodId)
                        {
                            case 974898854: 
                                ((ISimulationObserver)grain).ReportResults((Int64)arguments[0], (Int32)arguments[1], (Int32)arguments[2], (System.Collections.Generic.Dictionary<Int64,Int32>)arguments[3], (System.Collections.Generic.Dictionary<Int64,Int32>)arguments[4]); return true;
                            default: 
                                throw new NotImplementedException("interfaceId="+interfaceId+",methodId="+methodId);
                        }
                    default:
                        throw new System.InvalidCastException("interfaceId="+interfaceId);
                }
        }
        
        public static string GetMethodName(int interfaceId, int methodId)
        {

            switch (interfaceId)
            {
                
                case -2015988020:  // ISimulationObserver
                    switch (methodId)
                    {
                        case 974898854:
                            return "ReportResults";
                    case -606142484:
                            return "GetProperties";
                    
                        default: 
                            throw new NotImplementedException("interfaceId="+interfaceId+",methodId="+methodId);
                    }

                default:
                    throw new System.InvalidCastException("interfaceId="+interfaceId);
            }
        }
    }
    
    [Serializable()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.0.814.60418")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public class SimulationObserverProperties
    {
        

            public Dictionary<string,object> AsDictionary()
            {  
                var retValue = new Dictionary<string,object>();
                return retValue;
            }
    }
}
#pragma warning restore 162
#pragma warning restore 219
#pragma warning restore 693
#pragma warning restore 1591
#pragma warning restore 1998
#endif
