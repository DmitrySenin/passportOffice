;(function() {
	'use strict';

	angular
		.module('main')
		.controller('UsersCtrl', UsersCtrl);

	function UsersCtrl() {
		var vm = this;

		createCredentialsFields(vm);

		/**
		 * Add variables to store credentials to scope.
		 * @return {Object} Object where credentials should be added.
		 */
		function createCredentialsFields(scope) {
			scope.username = '';
			scope.password = '';
		}
	}

})();