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
		 * Create URL to API to getting passport data.
		 * @param {Numeric} pageSize Amount of personal information records per one page.
		 * @param {Numeric} pagenumber Number of requested page of date.
		 * @param {Boolean} fullSort Flag to sorting data by all fields.
		 * @return {string} URL for requesting personal data from server. 
		 */
		this.BuildPersonalInfoGetURL = function(pageSize, pageNumber, fullSort) {
			return BASE_URL + "personalinfo/" + pageSize + "/" + pageNumber + "/" + fullSort;
		}

		/**
		 * Create URL to API to login user.
		 * @return {String} URL to server to check credentials.
		 */
		this.BuildLoginURL = function() {
			return BASE_URL + 'token';
		}
	}
})();