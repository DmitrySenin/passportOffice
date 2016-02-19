;(function () {
	angular
		.module('main', ['ui.bootstrap', 'infinite-scroll', 'angularSpinner'])
		.constant('EventNames', {
			'Search' : 'search',
			'AuthAdmin' : 'authadmin'
		})
		.config(function($httpProvider) {
			$httpProvider.interceptors.push('AuthInterceptorService');
		});
})();