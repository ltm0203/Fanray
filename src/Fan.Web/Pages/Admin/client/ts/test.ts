﻿import Vue from 'vue'
import axios from 'axios'


class App extends Vue {
    //adminMixin: any;
    //mixins = [adminMixin];
    drawer = null;
    snackbar = {
        show: false,
        text: '',
        color: '',
        timeout: 0,
    };
    adminNavs: any;

    get tok() {
        return (document.querySelector('input[name="__RequestVerificationToken"][type="hidden"]') as HTMLInputElement).value;
    }

    get headers() {
        return { headers: { 'XSRF-TOKEN': this.tok } };
    }

    mounted() {
        console.log('inside mounted');
        this.initActiveNav();
    }

    /**
         * Make the current admin side nav active.
         */
    initActiveNav() {
        var url = window.location.pathname;
        this.adminNavs.forEach(function (nav) {
            // nav.active = url.startsWith(nav.url);
        });
    }
    logout() {
        console.log('logout');
        axios.post('/home/logout', null, this.headers)
            .then(function (response) {
                window.location.href = '/';
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    /**
     * 
     * @param {any} text
     * @param {any} timeout  Use 0 to keep open indefinitely.
     * @param {any} color
     */
    toast(text, timeout = 3000, color = 'silver') {
        this.snackbar.show = true;
        this.snackbar.text = text;
        this.snackbar.color = color;
        this.snackbar.timeout = timeout;
    }
    toastError(text, timeout = 3000) {
        this.snackbar.show = true;
        this.snackbar.text = text;
        this.snackbar.color = 'red';
        this.snackbar.timeout = timeout;
    }
}
