// NOTE:
// update the configuration with the ip address of the gateway
// you have been given.
const GatewayIpAddress = '10.0.0.45';//[YOUR_GATEWAY_ADDRESS_HERE] 

export default {
  controlPanelBaseUrl: `http://${GatewayIpAddress}:3000/api`,
  mqttBrokerUrl: `tcp://${GatewayIpAddress}:1883`
};
