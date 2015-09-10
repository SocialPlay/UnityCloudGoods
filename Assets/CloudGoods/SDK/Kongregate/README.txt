Code to be added to the index.html built in WebGL 

To Header:

    <script src='https://cdn1.kongregate.com/javascripts/kongregate_api.js'></script>

To Body:

      <script type="text/javascript">
    var kongregate;

    // Called when the API is finished loading
    function onLoadCompleted() {
        kongregate = kongregateAPI.getAPI();
    }

    function GetKongregateAPI() {
        var params = kongregate.services.getUsername() + "|" + kongregate.services.getUserId() + "|" + kongregate.services.getGameAuthToken();
        SendMessage("KongregateAPI", "OnKongregateAPILoaded", params);
    }

    function KongregateOnPurchaseResult(result) {
        console.log("result: " + result);
        SendMessage("PremiumCurrencyBundleStore", "KongregatePurchaseResponse", result.success.toString());
    }

    function KongregatePurchase(data) {
        console.log("Purchase js: " + data);
        kongregate.mtx.purchaseItemsRemote(data, KongregateOnPurchaseResult);
    }

    kongregateAPI.loadAPI(onLoadCompleted);
      </script>