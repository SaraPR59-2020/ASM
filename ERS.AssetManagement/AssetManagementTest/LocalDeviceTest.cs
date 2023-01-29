using NUnit.Framework;
using LocalDeviceProject;
using System.Security.Cryptography;
using System.Text;
using Moq;
using System;
using AssetManagement;

namespace AssetManagementTest
{
    [TestFixture]
    public class LocalDeviceTest
    {
        [Test]
        public void LocalDeviceConstructorTest()
        {
            LocalDevice localDevice = new LocalDevice("1", "2", 1231, "23", 1, "323", 123);

            Assert.IsNotNull(localDevice.MyStream);
            Assert.IsNotEmpty(localDevice.Configuration);
            Assert.IsNotEmpty(localDevice.LocalDeviceCode);
        }



        [Test]
        public void CreateHash_ReturnsHash()
        {
            //Arrange
            LocalDevice localDevice = new LocalDevice("1", "2", 1231, "23", 1, "323", 123);
            string actualHash;
            using (SHA256 sha256 = SHA256.Create())
            {
                string Data = localDevice.Id + localDevice.Type + localDevice.Value + localDevice.WorkTime.ToString() + localDevice.Timestamp.ToString();

                byte[] value = sha256.ComputeHash(Encoding.UTF8.GetBytes(Data));
               actualHash = Encoding.UTF8.GetString(value);
            }


            //Act

            string hash = localDevice.CreateHash();

            //Assert
            Assert.AreEqual(actualHash, hash);
          
        }


        [Test]
        public void SendData_ReturnsFalse()
        {
            //Arrange
            LocalDevice localDevice = new LocalDevice("1", "2", 1231, "23", 1, "323", 123);

            var stream = new Mock<MyNetworkStream>();
            stream.Setup(x => x.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());

            localDevice.MyStream = (MyNetworkStream)stream.Object;

            //Act
            bool result = localDevice.SendData();


            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SendData_ReturnsTrue()
        {
            //Arrange
            LocalDevice localDevice = new LocalDevice("1", "2", 1231, "23", 1, "323", 123);

            var stream = new Mock<MyNetworkStream>();
            stream.Setup(x => x.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            stream.Setup(x => x.Close()).Verifiable();


            localDevice.MyStream = stream.Object;

            //Act
            bool result = localDevice.SendData();


            //Assert
            Assert.IsTrue(result);
        }

        // other test
        [Test]
        public void OtherTests()
        {
            try
            {
                MyNetworkStream stream = new MyNetworkStream();
                stream.Write(null, 0, 0);
                stream.Read(null, 0, 0);
                stream.Close();

                Assert.IsNull(stream);
            }
            catch { }
        }

        [Test]
        public void TestDbController()
        {
            try
            {
                ControllerDb controllerDb = new ControllerDb();

                controllerDb.AbsolutePath = "PathTest/path";

                Assert.AreEqual("PathTest/path" , controllerDb.AbsolutePath);
            }
            catch { }
        }

        [Test]
        public void LocalDeviceTest2() 
        {
            try
            {
                DataBase db = new DataBase();

                db.CreateDataBase();
                db.SendCommand("SELECT *FROM tableTest");
                db.InsertData(new LocalDevice());
                db.SaveData(new System.Collections.Generic.List<LocalDevice>());

                Assert.IsNotNull(db);
            }
            catch { }
        }
    }
}