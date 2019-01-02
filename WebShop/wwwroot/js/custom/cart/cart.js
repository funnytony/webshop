var Cart = (function () {
    var _properties = {
        // Ссылка на метод добавления товара в корзину
        addToCartLink: '',
        // Ссылка на получение представления корзины
        getCartViewLink: '',
        // Ссылка на удаление товара из корзины
        removeFromCartLink: '',
        // Ссылка на уменьшение количества товаров
        decrementLink: ''
    };

    var showToolTip = function (button) {
        // Отображаем тултип
        button.tooltip({
            title: "Добавлено в корзину"
        }).tooltip('show');

        // Дестроим его через 0.5 секунды
        setTimeout(function () {
            button.tooltip('destroy');
        }, 500);
    };

    var refreshCartView = function () {
        // Получаем контейнер корзины
        var container = $("#cartContainer");
        // Получение представления корзины
        $.get(_properties.getCartViewLink)
            .done(function (result) {
                // Обновление html 
                container.html(result);
            })
            .fail(function () { console.log('refreshCartView error'); });
    };

    var addToCart = function (event) {
        var button = $(this);
        // Отменяем дефолтное действие
        event.preventDefault();
        // Получение идентификатора из атрибута
        var id = button.data('id');
        // Вызов метода контроллера
        $.get(_properties.addToCartLink + '/' + id)
            .done(function () {
                // Отображаем сообщение, что товар добавлен в корзину
                showToolTip(button);
                // В случае успеха – обновляем представление
                refreshCartView();
            })
            .fail(function () { console.log('addToCart error'); });
    };

    var refreshTotalPrice = function () {
        var total = 0;
        $('.cart_total_price').each(function () {
            var price = parseFloat($(this).data('price'));
            total += price;
        });
        var value = total.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' });
        $('.totalOrderSum').html(value);
    };

    var removeFromCart = function (event) {

        var button = $(this);
        // Отменяем дефолтное действие
        event.preventDefault();

        // Получение идентификатора из атрибута
        var id = button.data('id');
        $.get(_properties.removeFromCartLink + '/' + id)
            .done(function () {
                button.closest('tr').remove();
                // В случае успеха – обновляем представление
                refreshCartView();
                refreshTotalPrice();
            })
            .fail(function () { console.log('addToCart error'); });
    };

    var refreshPrice = function (container) {

        // Получаем количество
        var quantity = parseInt($('.cart_quantity_input', container).val());
        // Получаем цену
        var price = parseFloat($('.cart_price', container).data('price'));
        // Рассчитываем общую стоимость для отображения в виде валюты
        var totalPrice = quantity * price;
        var value = totalPrice.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' });

        // Сохраняем стоимость для поля “Итого”
        $('.cart_total_price', container).data('price', totalPrice);
        // Меняем значение
        $('.cart_total_price', container).html(value);

        refreshTotalPrice();
    };

    var incrementItem = function (event) {
        var button = $(this);
        // Строка товара
        var container = button.closest('tr');
        // Отменяем дефолтное действие
        event.preventDefault();
        // Получение идентификатора из атрибута
        var id = button.data('id');

        // Вызов метода контроллера
        $.get(_properties.addToCartLink + '/' + id).done(function () {
            // Получаем значение
            var value = parseInt($('.cart_quantity_input', container).val());
            // Увеличиваем его на 1
            $('.cart_quantity_input', container).val(value + 1);
            // Обновляем цену
            refreshPrice(container);
            refreshTotalPrice();
            // В случае успеха – обновляем представление
            refreshCartView();
        }).fail(function () { console.log('addToCart error'); });
    };

    var decrementItem = function (event) {
        var button = $(this);
        // Строка товара
        var container = button.closest('tr');
        // Отменяем дефолтное действие
        event.preventDefault();
        // Получение идентификатора из атрибута
        var id = button.data('id');
        $.get(_properties.decrementLink + '/' + id)
            .done(function () {
                var value = parseInt($('.cart_quantity_input', container).val());
                if (value > 1) {
                    // Уменьшаем его на 1
                    $('.cart_quantity_input', container).val(value - 1);
                    refreshPrice(container);
                } else {
                    container.remove();
                    refreshTotalPrice();
                }
                // В случае успеха – обновляем представление
                refreshCartView();
            }).fail(function () { console.log('addToCart error'); });
    };

    var initEvents = function () {
        $('a.callAddToCart').on('click', addToCart);
        // Кнопка «Удалить товар из корзины»
        $('.cart_quantity_delete').on('click', removeFromCart);
        // Кнопка «+»
        $('.cart_quantity_up').on('click', incrementItem);
        // Кнопка «-»
        $('.cart_quantity_down').on('click', decrementItem);
    };

    return {
        init: function (properties) {
            // Копируем свойства
            $.extend(_properties, properties);
            // Инициализируем перехват события
            initEvents();
        }
    }
})();