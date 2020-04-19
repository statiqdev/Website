NoSidebar: true
---
Using the following Statiq projects for commercial purposes requires the purchase of a private license:

- [Statiq Web](/web)
- [Statiq Docs](/docs)

The cost for a private license is **$50.00 per developer** and covers all releases of the current major version. See the FAQ below for more details.

<div>
    <script src="https://js.stripe.com/v3/" data-no-mirror></script>
    <div class="form-inline">
        <label class="my-1 mr-2" for="quantityInput" class="mr-2">Number Of Developers:</label>
        <input id="quantityInput" type="number" min="1" onchange="changeQuantity(this.value)" value="1" class="mr-2"></input>
        <button class="btn btn-primary" id="checkout-button-sku_H7XbjLJDzHVInz" role="link">Buy A Commercial-Use License</button>
    </div>
    <div id="error-message"></div>
    <script>
        var quantity = 1;
        function changeQuantity(value) {
            quantity = value;
        }
        (function() {
            var stripe = Stripe('pk_live_4tiVivY5ixgmXsAuM5khf36i');
            var checkoutButton = document.getElementById('checkout-button-sku_H7XbjLJDzHVInz');
            checkoutButton.addEventListener('click', function () {
                // When the customer clicks on the button, redirect
                // them to Checkout.
                stripe.redirectToCheckout({
                items: [{sku: 'sku_H7XbjLJDzHVInz', quantity: Number(quantity)}],
                // Do not rely on the redirect to the successUrl for fulfilling
                // purchases, customers may not always reach the success_url after
                // a successful payment.
                // Instead use one of the strategies described in
                // https://stripe.com/docs/payments/checkout/fulfillment
                successUrl: 'https://statiq.dev/license/thank-you',
                cancelUrl: 'https://statiq.dev/license',
                })
                .then(function (result) {
                if (result.error) {
                    // If `redirectToCheckout` fails due to a browser or network
                    // error, display the localized error message to your customer.
                    var displayError = document.getElementById('error-message');
                    displayError.textContent = result.error.message;
                }
                });
            });
        })();
    </script>
</div>

<?!^ "https://raw.githubusercontent.com/statiqdev/Statiq.Web/master/LICENSE-FAQ.md" /?>