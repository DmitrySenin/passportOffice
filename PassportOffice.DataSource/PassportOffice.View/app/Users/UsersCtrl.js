;(function() {
	'use strict';

	angular
		.module('main')
		.controller('UsersCtrl', UsersCtrl);

	function UsersCtrl() {
		var vm = this;

		createCredentialsFields();

		function createCredentialsFields() {
			vm.username = '';
			vm.password = '';
		}
	}

})();