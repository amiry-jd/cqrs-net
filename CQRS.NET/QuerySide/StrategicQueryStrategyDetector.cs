// using System;

// namespace CQRS.NET
// {
//     public class StrategicQueryStrategyDetector:IStrategyDetector<StrategicQuery> {

//         public Type FindHandlerType(StrategicQuery input) {
//             return input.Value == 1 ? typeof(StrategyNoOneQueryHandler) : typeof(StrategyNoTwoQueryHandler);
//         }

//     }
// }