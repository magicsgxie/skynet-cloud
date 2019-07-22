using System;
using System.Collections.Generic;
using System.Text;

namespace Skynet.Cloud.ConnectorBase.Test
{
    public static class OracleTestHelper
    {
        public static string SingleServerVCAP = @"
{
      'p-oracle': [
        {
          'credentials': {
            'hostname': '192.168.0.90',
            'port': 3306,
            'name': 'cf_b4f8d2fa_a3ea_4e3a_a0e8_2cd040790355',
            'username': 'Dd6O1BPXUHdrmzbP',
            'password': '7E1LxXnlH2hhlPVt',
            'uri': 'oracle:thin:@//192.168.0.90:3306:1521/xe?reconnect=true',
            'jdbcUrl': 'jdbc:oracle:thine:@192.168.0.90:1521/xe?user=Dd6O1BPXUHdrmzbP&password=7E1LxXnlH2hhlPVt'
          },
          'syslog_drain_url': null,
          'label': 'p-oracle',
          'provider': null,
          'plan': '100mb-dev',
          'name': 'spring-cloud-broker-db',
          'tags': [
            'oracle',
            'relational'
          ]
        }
      ]
}";

        public static string TwoServerVCAP = @"
{
      'p-oracle': [
        {
          'credentials': {
            'hostname': '192.168.0.90',
            'port': 3306,
            'name': 'cf_b4f8d2fa_a3ea_4e3a_a0e8_2cd040790355',
            'username': 'Dd6O1BPXUHdrmzbP',
            'password': '7E1LxXnlH2hhlPVt',
            'uri': 'oracle:thin:@Dd6O1BPXUHdrmzbP:7E1LxXnlH2hhlPVt@192.168.0.90:3306/cf_b4f8d2fa_a3ea_4e3a_a0e8_2cd040790355?reconnect=true',
            'jdbcUrl': 'jdbc:oracle:thine:@192.168.0.90:3306/cf_b4f8d2fa_a3ea_4e3a_a0e8_2cd040790355?user=Dd6O1BPXUHdrmzbP&password=7E1LxXnlH2hhlPVt'
          },
          'syslog_drain_url': null,
          'label': 'p-oracle',
          'provider': null,
          'plan': '100mb-dev',
          'name': 'spring-cloud-broker-db',
          'tags': [
            'oracle',
            'relational'
          ]
        },
        {
          'credentials': {
            'hostname': '192.168.0.90',
            'port': 3306,
            'name': 'cf_b4f8d2fa_a3ea_4e3a_a0e8_2cd040790355',
            'username': 'Dd6O1BPXUHdrmzbP',
            'password': '7E1LxXnlH2hhlPVt',
            'uri': 'oracle:thin:@192.168.0.91:1521:xe?user=Dd6O1BPXUHdrmzbP&password=7E1LxXnlH2hhlPVt',
            'jdbcUrl': 'jdbc:oracle:thin:@192.168.0.90:3306:xe?user=Dd6O1BPXUHdrmzbP&password=7E1LxXnlH2hhlPVt'
          },
          'syslog_drain_url': null,
          'label': 'p-oracle',
          'provider': null,
          'plan': '100mb-dev',
          'name': 'spring-cloud-broker-db2',
          'tags': [
            'oracle',
            'relational'
          ]
        }
      ]
}";
    }
}
