# AllyBankAPIExample
Get from Ally Invest Bank an XML return with detailed balance and holding information for each account associated with a user.

# Ally Invest
Ally Invest API uses similar functionality that I found in some Twitter API example applications. So, I found it very helpful to use them and implement tthem for Ally Invest API. The bank provide examples in other languages such as Java, Node.js, PHP, R, and Ruby, but no C#. Here you have a simple one. All you need is to customize your own C# application. 

# Disclaimer
This software is not related to the bank, however, it has been developed based on their requirements placed in their website (see the links below). It is provided as-is for personal, academic and learning purposes.

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
### Example Result
This is an example of the output:
![alt text](https://raw.githubusercontent.com/angelm83a/AllyBankAPIExample/master/resultXML.png)

### More Help

 * Helping code/question/answer: https://stackoverflow.com/a/27108442/7536542
 * Helping project: https://www.codeproject.com/Articles/247336/Twitter-OAuth-authentication-using-Net
 * A PHP example: https://gist.github.com/tagroup/4006361
