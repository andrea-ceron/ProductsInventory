using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Kafka.DependencyInjection;

namespace ProductsInventory.Business.Kafka;

public class KafkaTopicsInput : AbstractInputKafkaTopics
{
	[Required]
	[ConfigurationKeyName("RawMaterial")] 
	public string RawMaterial { get; set; }  

	public override IEnumerable<string> GetTopics() => [RawMaterial];
}

public class KafkaTopicsOutput : AbstractOutputKafkaTopics
{
	[Required]
	[ConfigurationKeyName("EndProduct")]
	public string EndProduct { get; set; }

	public override IEnumerable<string> GetTopics() => [EndProduct];
}


