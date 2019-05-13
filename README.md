# AllyBankAPIExample
Get from Ally Bank Invest an XML return with detailed balance and holding information for each account associated with a user.

# Ally Invest
Ally Bank API uses similar functionality as found in some Twitter API applications. So, I found it very helpful to use a Twitter's application and implement it for Ally Invest API.

# Documentation
  - https://www.ally.com/api/invest/documentation/accounts-get/
  - https://www.ally.com/api/invest/documentation/accounts-id-get/
  - https://www.ally.com/api/invest/documentation/streaming-market-quotes-get-post/

### Technicality

According to Ally's website: "The keys you received when you registered your application are all that is required." See: https://www.ally.com/api/invest/documentation/oauth/

Thus, the variables below need to be filled with your personal keys provided by the bank
```sh
var oauth_consumer_key = "5LaqR_YOUR_OWN_KEY_HERE_46";
var oauth_consumer_secret = "Ji_YOUR_OWN_KEY_HERE_328U5";
var oauth_token = "FKz_YOUR_OWN_KEY_HERE_hQ7";
var oauth_token_secret = "YDG_YOUR_OWN_KEY_HERE_Mo80";
 ```

### More Help

 * Helping code/question/answer: https://stackoverflow.com/a/27108442/7536542
 * Helping project: https://www.codeproject.com/Articles/247336/Twitter-OAuth-authentication-using-Net
