;(function() {
	angular
		.module('main')
		.service('URLService', URLService);

	/**
	 * Encapsulates logic of URL service. 
	 */
	function URLService() {

		/**
		 * URL of server.
		 * @type {String}
		 */
		var BASE_URL = "http://localhost:57336/";

		/**
		 * Create URL to controller of personal information.
		 * @return {String} URL to make requests to manage persoanal data.
		 */
		function BuildPersonalInfoURL() {
			return BASE_URL + "personalinfo/";
		}

		/**
		 * Create URL to API to getting passport data.
		 * @param {Numeric} pageSize Amount of personal information records per one page.
		 * @param {Numeric} pagenumber Number of requested page of date.
		 * @param {Boolean} fullSort Flag to sorting data by all fields.
		 * @return {string} URL for requesting personal data from server. 
		 */
		this.BuildPersonalInfoGetURL = function(pageSize, pageNumber, fullSort) {
			return BuildPersonalInfoURL() + pageSize + "/" + pageNumber + "/" + fullSort;
		}

		/**
		 * Create URL to API to login user.
		 * @return {String} URL to server to check credentials.
		 */
		this.BuildLoginURL = function() {
			return BASE_URL + 'token';
		}

		/**
		 * Create URL to API to checl that user is administrator.
		 */
		this.BuildIsAdminURL = function() {
			return BASE_URL + "users/isadmin"
		}
	}
})();